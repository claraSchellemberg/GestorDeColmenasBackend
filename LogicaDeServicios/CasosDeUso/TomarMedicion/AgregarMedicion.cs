using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.DTOs.Arduino;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.TomarMedicion
{
    public class AgregarMedicion : IAgregar<DataArduinoDto>
    {
        private IRepositorioCuadro _repoCuadros;
        private IRepositorioColmena _repoColmenas;
        private IRepositorioSensor _repoSensores;
        private IRepositorioRegistroSensor _repoRegistrosSensores;
        private IRepositorioRegistroMedicionColmena _repoRegistroMedicionColmena;
        private IRepositorioNotificacion _repoNotificaciones;
        private IGeneradorNotificaciones _generadorNotificaciones;

        public AgregarMedicion(IRepositorioCuadro repoCuadros, 
                        IRepositorioColmena repoColmenas, IRepositorioSensor repoSensores, 
                        IRepositorioRegistroSensor repoRegistrosSensores,
                        IRepositorioRegistroMedicionColmena repositorioRegistroMedicionColmena,
                        IRepositorioNotificacion repoNotificaciones,
                        IGeneradorNotificaciones generadorNotificaciones)
        {
            _repoCuadros = repoCuadros;
            _repoColmenas = repoColmenas;
            _repoSensores = repoSensores;
            _repoRegistrosSensores = repoRegistrosSensores;
            _repoRegistroMedicionColmena = repositorioRegistroMedicionColmena;
            _repoNotificaciones = repoNotificaciones;
            _generadorNotificaciones = generadorNotificaciones;
        }
        
        public void Agregar(DataArduinoDto obj)
        {
            //busca colmena por sensor
            Sensor sensor = _repoSensores.ObtenerElementoPorId(obj.idSensor);
            sensor.Colmena = _repoColmenas.ObtenerElementoPorId(sensor.ColmenaId);
            
            // Crear la medición de colmena
            if(obj.peso>0 || obj.tempExterna>0)
            {
                MedicionColmena medicionColmena = new MedicionColmena
                {
                    Peso = obj.peso,
                    TempExterna = obj.tempExterna,
                    FechaMedicion = DateTime.Now
                };
                _repoColmenas.AgregarMedicion(medicionColmena, sensor.Colmena);

                RegistroMedicionColmena registroMedicionColmena = new RegistroMedicionColmena
                {
                    MedicionColmena = medicionColmena,
                    FechaRegistro = DateTime.Now,
                    EstaPendiente = true
                };
                _repoRegistroMedicionColmena.Agregar(registroMedicionColmena);
                registroMedicionColmena.ControlarValores();
            }
            else if(obj.temp1>0 || obj.temp2>0 || obj.temp3 > 0)
            {
                //agrega al cuadro
                sensor.Cuadro = _repoCuadros.ObtenerElementoPorId(sensor.CuadroId);
                SensorPorCuadro medicionDeCuadro = new SensorPorCuadro
                {
                    Sensor = sensor,
                    TempInterna1 = obj.temp1,
                    TempInterna2 = obj.temp2,
                    TempInterna3 = obj.temp3,
                    FechaMedicion = DateTime.Now
                };
                _repoCuadros.AgregarMedicionDeCuadro(medicionDeCuadro, sensor.Cuadro);

                RegistroSensor registro = new RegistroSensor
                {
                    sensorPorCuadro = medicionDeCuadro,
                    FechaRegistro = DateTime.Now,
                    EstaPendiente = true
                };
                
                // Controlar si esta en rango borde
                registro.ControlarValores();
                
                // Agregar el registro ANTES de verificar colmena
                _repoRegistrosSensores.Agregar(registro);
                
                // AQUI VIENE LA LOGICA NUEVA: Verificar si generar notificacion a nivel colmena
                VerificarEstadoColmena(sensor.Colmena, registro);
            }   
        }

        /// <summary>
        /// Verifica si todos los cuadros de la colmena tienen su última medición en rango borde.
        /// Si es así, genera una notificación y actualiza el estado de la colmena.
        /// </summary>
        private void VerificarEstadoColmena(Colmena colmena, RegistroSensor registroActual)
        {
            // PASO 1: Obtener todos los cuadros de la colmena
            // Esto es necesario porque necesitamos verificar el estado de TODA la colmena
            var cuadrosDeColmena = colmena.Cuadros;
            
            if (cuadrosDeColmena == null || cuadrosDeColmena.Count == 0)
            {
                return; // No hay cuadros para verificar
            }

            // PASO 2: Para cada cuadro, obtener el último registro de sensor
            // Recorremos todos los cuadros porque cada uno tiene mediciones independientes
            var ultimasmedicionesQueBordean = new List<RegistroSensor>();
            
            foreach (var cuadro in cuadrosDeColmena)
            {
                // Obtener TODOS los registros de sensores para este cuadro
                // (necesitamos toda la lista para ordenar y obtener el último)
                var ultimoRegistro = _repoRegistrosSensores.ObtenerUltimoPorCuadro(cuadro.Id);


                if (ultimoRegistro != null && ultimoRegistro.ValorEstaEnRangoBorde)
                {
                    ultimasmedicionesQueBordean.Add(ultimoRegistro);
                }
            }

            // PASO 4: Si todos los cuadros tienen medición en rango borde, generar notificación
            // Comparamos la cantidad de cuadros con mediciones en rango borde
            // Si son iguales, significa que TODOS los cuadros están en rango borde
            if (ultimasmedicionesQueBordean.Count == cuadrosDeColmena.Count)
            {
                string mensaje = $"ALERTA: Todos los cuadros de la colmena {colmena.Nombre} están en rango borde de temperatura.";
                GenerarNotificacion(mensaje, colmena, registroActual);
                
                // Opcional: Actualizar el estado de la colmena
                colmena.Condicion = CondicionColmena.EN_PELIGRO;
                _repoColmenas.Actualizar(colmena);
            }
        }

        private void GenerarNotificacion(string mensaje, Colmena colmena, Registro registroAsociado)
        {
            try
            {
                // PASO 1: Crear la notificación
                var notificacion = new Notificacion(mensaje, registroAsociado);
                notificacion.ValidarNotificacion();

                // PASO 2: Guardar en la base de datos
                _repoNotificaciones.Agregar(notificacion);

                // PASO 3: Notificar a todos los observadores suscritos
                // El controlador de notificaciones estará escuchando y lo recibirá aquí
                _generadorNotificaciones.NotificarObservadores(notificacion);
            }
            catch (Exception ex)
            {
                // Log del error - puedes usar un logger aquí
                Console.WriteLine($"Error al generar notificación: {ex.Message}");
            }
        }
    }
}

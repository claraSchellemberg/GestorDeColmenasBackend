using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.Notificaciones;
using LogicaDeNegocios.InterfacesRepositorio.Registros;
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
    public class AgregarMedicion : IAgregar<DataArduinoDto, DataArduinoDto>
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

        public DataArduinoDto Agregar(DataArduinoDto obj)
        {
            Sensor sensor = _repoSensores.ObtenerElementoPorId(obj.idSensor);
            //sensor.Colmena = _repoColmenas.ObtenerElementoPorId(sensor.ColmenaId);
            
            if ((obj.peso != null && obj.peso >= 0 && obj.tipoSensor=="peso") || (obj.tempExterna != null && obj.tempExterna > 0))
            {
                MedicionColmena medicionColmena = new MedicionColmena
                {
                    Peso = (float)obj.peso,
                    TempExterna = (float)obj.tempExterna,
                    FechaMedicion = DateTime.Now
                };
                _repoColmenas.AgregarMedicion(medicionColmena, sensor.Colmena);

                RegistroMedicionColmena registroMedicionColmena = new RegistroMedicionColmena
                {
                    MedicionColmena = medicionColmena,
                    FechaRegistro = DateTime.Now,
                    EstaPendiente = true
                };
                
                registroMedicionColmena.ControlarValores();
                _repoRegistroMedicionColmena.Agregar(registroMedicionColmena);

                // Generate notification for EACH alert
                if (registroMedicionColmena.ValorEstaEnRangoBorde)
                {
                    foreach (var mensaje in registroMedicionColmena.MensajesAlerta)
                    {
                        GenerarNotificacion(mensaje, sensor.Colmena, registroMedicionColmena);
                    }
                }
            }
            if ((obj.tempInterna1 != null && obj.tempInterna1 > 0) || (obj.tempInterna2 != null && obj.tempInterna2 > 0) || (obj.tempInterna3 != null && obj.tempInterna3 > 0))
            {
                sensor.Cuadro = _repoCuadros.ObtenerElementoPorId(sensor.CuadroId);
                SensorPorCuadro medicionDeCuadro = new SensorPorCuadro
                {
                    Sensor = sensor,
                    TempInterna1 = (float)obj.tempInterna1,
                    TempInterna2 = (float)obj.tempInterna2,
                    TempInterna3 = (float)obj.tempInterna3,
                    FechaMedicion = DateTime.Now
                };
                _repoCuadros.AgregarMedicionDeCuadro(medicionDeCuadro, sensor.Cuadro);

                RegistroSensor registro = new RegistroSensor
                {
                    SensorPorCuadro = medicionDeCuadro,
                    FechaRegistro = DateTime.Now,
                    EstaPendiente = true
                };
                
                registro.ControlarValores();
                _repoRegistrosSensores.Agregar(registro);

                // Generate notification for EACH alert
                if (registro.ValorEstaEnRangoBorde)
                {
                    foreach (var mensaje in registro.MensajesAlerta)
                    {
                        GenerarNotificacion(mensaje, sensor.Colmena, registro);
                    }
                }
                
                VerificarEstadoColmena(sensor.Colmena, registro);
            }
            return obj;
        }

        // Verifica si todos los cuadros de la colmena tienen su última medición en 
        // rango borde.
        // Si es así, genera una notificación y actualiza el estado de la colmena.
        private void VerificarEstadoColmena(Colmena colmena, RegistroSensor registroActual)
        {
            // PASO 1: Obtener todos los cuadros de la colmena
            // Esto es necesario porque necesitamos verificar el estado de
            // TODA la colmena
            var cuadrosDeColmena = colmena.Cuadros;

            if (cuadrosDeColmena == null || cuadrosDeColmena.Count == 0)
            {
                return; // No hay cuadros para verificar
            }

            // PASO 2: Para cada cuadro, obtener el último registro de sensor
            // Recorremos todos los cuadros porque cada uno tiene mediciones
            // independientes
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

            // PASO 4: Si todos los cuadros tienen medición en rango borde, generar
            // notificación
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
                var notificacion = new Notificacion(mensaje, registroAsociado, colmena.Apiario.Usuario);
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

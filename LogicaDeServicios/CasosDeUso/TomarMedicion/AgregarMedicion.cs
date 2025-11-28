using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.DTOs.Arduino;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.TomarMedicion
{
    public class AgregarMedicion : IAgregar<DataArduinoDto>
    {
        //aca agrega a los sesores por cuadro y al otro donde se agrega el peso y temp externa
        //casos de usos son el negocio

        //con el registrosetdto(aca trae el id de sensor que viene hardcodeado) ir a buscar la colmena,
        //me devuelve la placa que corresponde ese sensor
        //aca agrego nuevo csensor por cuador y cambio peso de colmena llamo a los reepo
        //correspondientes (colmena y sensor por cuadro)
        //en el caso de uso llamo a todos los repos necesarios y los repos guardan en la base de datos 
        private IRepositorioCuadro _repoCuadros;
        private IRepositorioColmena _repoColmenas;
        private IRepositorioSensor _repoSensores;
        public AgregarMedicion(IRepositorioCuadro repoCuadros, 
                        IRepositorioColmena repoColmenas, IRepositorioSensor repoSensores)
        {
            _repoCuadros = repoCuadros;
            _repoColmenas = repoColmenas;
            _repoSensores = repoSensores;
        }
        public void Agregar(DataArduinoDto obj)
        {
            //busca colmena por sensor
            Sensor sensor = _repoSensores.ObtenerElementoPorId(obj.idSensor);

            Colmena colmena = sensor.colmena;
            // Crear la medición de colmena
            MedicionColmena medicionColmena = new MedicionColmena
            {
                Peso = obj.peso,
                TempExterna = obj.tempExterna,
                FechaMedicion = DateTime.Now
            };
            _repoColmenas.AgregarMedicion(medicionColmena, colmena);
            //agrega al cuadro
            //agarramos el cuadro que tiene el sensor
            Cuadro cuadro = sensor.cuadro;
            SensorPorCuadro medicionDeCuadro = new SensorPorCuadro
            {
                sensor = sensor,
                TempInterna1 = obj.temp1,
                TempInterna2 = obj.temp2,
                TempInterna3 = obj.temp3,
                FechaMedicion = DateTime.Now
            };
            _repoCuadros.AgregarMedicionDeCuadro(medicionDeCuadro, cuadro);

        }
    }
}

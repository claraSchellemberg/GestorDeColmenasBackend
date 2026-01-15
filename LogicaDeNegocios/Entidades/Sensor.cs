namespace LogicaDeNegocios.Entidades
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string TipoSensor { get; set; }        
        public int ColmenaId { get; set; }
        public int CuadroId { get; set; }
        public Colmena Colmena { get; set; }
        public Cuadro Cuadro { get; set; }

        public Sensor()
        {
        }

        public Sensor(string tipoSensor, Colmena colmena, Cuadro cuadro)
        {
            TipoSensor = tipoSensor;
            Colmena = colmena;
            Cuadro = cuadro;
        }
        public Sensor(string tipoSensor, int sensorId, Colmena colmena, Cuadro cuadro)
        {
            TipoSensor = tipoSensor;
            SensorId = sensorId;
            Colmena = colmena;
            Cuadro = cuadro;
        }
    }
}
namespace LogicaDeNegocios.Entidades
{
    public class Sensor
    {
        public string TipoSensor { get; set; }
        public int SensorId { get; set; }
        public Colmena Colmena;
        public Cuadro Cuadro;   
        public Sensor()
        {
        }
        public Sensor(string tipoSensor, int sensorId, Colmena colmena, Cuadro cuadro)
        {
            TipoSensor = tipoSensor;
            SensorId = sensorId;
            this.Colmena = colmena;
            this.Cuadro = cuadro;
        }
    }
}
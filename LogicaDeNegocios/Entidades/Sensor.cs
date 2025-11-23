namespace LogicaDeNegocios.Entidades
{
    public class Sensor
    {
        public string TipoSensor;
        public int SensorId;
        public Sensor()
        {
        }
        public Sensor(string tipoSensor, int sensorId)
        {
            TipoSensor = tipoSensor;
            SensorId = sensorId;
        }
    }
}
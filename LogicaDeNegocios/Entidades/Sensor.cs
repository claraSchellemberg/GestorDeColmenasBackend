namespace LogicaDeNegocios.Entidades
{
    public class Sensor
    {
        public string TipoSensor;
        public int SensorId;
        public Colmena colmena;
        public Cuadro cuadro;
        public Sensor()
        {
        }
        public Sensor(string tipoSensor, int sensorId, Colmena colmena, Cuadro cuadro)
        {
            TipoSensor = tipoSensor;
            SensorId = sensorId;
            this.colmena = colmena;
            this.cuadro = cuadro;
        }
    }
}
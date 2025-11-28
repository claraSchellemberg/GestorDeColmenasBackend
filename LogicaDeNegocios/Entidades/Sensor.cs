namespace LogicaDeNegocios.Entidades
{
    public class Sensor
    {
<<<<<<< HEAD
        public string TipoSensor;
        public int SensorId;
        public Colmena colmena;
        public Cuadro cuadro;
=======
        public string TipoSensor { get; set; }
        public int SensorId { get; set; }
        public Colmena Colmena;
        public Cuadro Cuadro;   
>>>>>>> origin/Develop
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
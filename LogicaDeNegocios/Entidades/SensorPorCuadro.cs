namespace LogicaDeNegocios.Entidades
{
    public class SensorPorCuadro
    {
        public Sensor sensor;

        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }
        public DateTime FechaMedicion { get; set; }

        public SensorPorCuadro()
        {
        }

        public SensorPorCuadro(Sensor sensor, float tempInterna1, float tempInterna2, float tempInterna3)
        {
            this.sensor = sensor;
            TempInterna1 = tempInterna1;
            TempInterna2 = tempInterna2;
            TempInterna3 = tempInterna3;
            this.FechaMedicion = DateTime.Now;
        }
    }
}
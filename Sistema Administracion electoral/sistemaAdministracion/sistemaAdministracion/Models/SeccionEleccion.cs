namespace sistemaAdministracion.Models
{
    public class SeccionEleccion
    {
        //(tabla intermedia)
        public int EleccionId { get; set; }
        public Eleccion Eleccion { get; set; }

        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }
    }
}

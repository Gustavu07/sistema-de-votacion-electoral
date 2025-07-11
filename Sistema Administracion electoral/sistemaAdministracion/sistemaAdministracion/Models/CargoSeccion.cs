namespace sistemaAdministracion.Models
{
    public class CargoSeccion
    {
        //tabla intermediaria
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }
    }
}

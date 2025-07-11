namespace sistemaAdministracion.Dtos
{
    public class EleccionDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CreateEleccionDto
    {
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
    }

}

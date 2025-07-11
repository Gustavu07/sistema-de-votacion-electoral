namespace sistemaAdministracion.Dtos
{
    public class CargoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CreateCargoDto
    {
        public string Nombre { get; set; }
    }

}

namespace sistemaAdministracion.Dtos
{
    public class PartidoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public string Color { get; set; }
    }

    public class CreatePartidoDto
    {
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public string Color { get; set; }
    }

}

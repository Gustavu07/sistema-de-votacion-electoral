namespace sistemaAdministracion.Dtos
{
    public class JuradoDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public int MesaElectoralId { get; set; }
    }

    public class CreateJuradoDto
    {
        public string NombreCompleto { get; set; }
        public int MesaElectoralId { get; set; }
    }

}

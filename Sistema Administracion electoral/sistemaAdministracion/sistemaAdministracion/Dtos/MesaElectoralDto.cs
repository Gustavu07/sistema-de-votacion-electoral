namespace sistemaAdministracion.Dtos
{
    public class MesaElectoralDto
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int RecintoId { get; set; }
    }

    public class CreateMesaElectoralDto
    {
        public int Numero { get; set; }
        public int RecintoId { get; set; }
    }

}

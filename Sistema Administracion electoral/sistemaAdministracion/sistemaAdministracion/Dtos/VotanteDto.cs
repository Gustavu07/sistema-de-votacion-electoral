namespace sistemaAdministracion.Dtos
{
    public class VotanteDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string ApellidoPaterno { get; set; }
        public int MesaElectoralId { get; set; }
    }

    public class CreateVotanteDto
    {
        public string NombreCompleto { get; set; }
        public string ApellidoPaterno { get; set; }
        public int RecintoId { get; set; } // necesario para encontrar las mesas disponibles
    }


}

namespace sistemaAdministracion.Dtos
{
    public class CandidaturaDto
    {
        public int Id { get; set; }
        public string NombreCandidato { get; set; }
        public string NombreCargo { get; set; }
        public string NombrePartido { get; set; }
    }

    public class CreateCandidaturaDto
    {
        public string NombreCandidato { get; set; }
        public int CargoId { get; set; }
        public int PartidoId { get; set; }
    }

}

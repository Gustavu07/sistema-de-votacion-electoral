namespace sistemaAdministracion.Dtos
{
    public class PapeletaDto
    {
        public int Id { get; set; }
        public int SeccionId { get; set; }
        public int EleccionId { get; set; }
        public List<int> CandidaturaIds { get; set; }
    }

    public class CreatePapeletaDto
    {
        public int SeccionId { get; set; }
        public int EleccionId { get; set; }
        public List<int> CandidaturaIds { get; set; }
    }

}

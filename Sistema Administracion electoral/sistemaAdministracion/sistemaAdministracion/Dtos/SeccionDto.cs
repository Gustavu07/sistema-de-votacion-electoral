namespace sistemaAdministracion.Dtos
{
    public class SeccionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Coordenadas { get; set; }
    }

    public class CreateSeccionDto
    {
        public string Nombre { get; set; }
        public string Coordenadas { get; set; }
    }

}

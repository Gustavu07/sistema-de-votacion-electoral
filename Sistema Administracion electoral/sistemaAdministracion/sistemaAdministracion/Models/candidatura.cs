using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaAdministracion.Models
{
    public class Candidatura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NombreCandidato { get; set; }

        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

        public int PartidoId { get; set; }
        public Partido Partido { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaAdministracion.Models
{
    public class Partido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public string Color { get; set; }

        public ICollection<Candidatura> Candidaturas { get; set; }
    }

}

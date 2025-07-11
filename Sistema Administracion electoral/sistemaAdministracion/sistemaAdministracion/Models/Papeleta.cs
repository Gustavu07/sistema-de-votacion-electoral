using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaAdministracion.Models
{
    public class Papeleta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }

        public int EleccionId { get; set; }
        public Eleccion Eleccion { get; set; }

        public ICollection<Candidatura> Candidatos { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaAdministracion.Models
{
    public class Eleccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }

        public ICollection<SeccionEleccion> SeccionesAfectadas { get; set; }
    }
}

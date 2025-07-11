using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaAdministracion.Models
{
    public class MesaElectoral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Numero { get; set; }

        public int RecintoId { get; set; }
        public Recinto Recinto { get; set; }

        public ICollection<Jurado> Jurados { get; set; }
        public ICollection<Votante> Votantes { get; set; }
    }

}

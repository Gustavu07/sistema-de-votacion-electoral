using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaAdministracion.Models
{
    public class Jurado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NombreCompleto { get; set; }

        public int MesaElectoralId { get; set; }
        public MesaElectoral MesaElectoral { get; set; }
    }

}

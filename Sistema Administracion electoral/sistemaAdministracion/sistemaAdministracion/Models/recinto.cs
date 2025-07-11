using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaAdministracion.Models
{
    public class Recinto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }

        public ICollection<MesaElectoral> Mesas { get; set; }
    }

}

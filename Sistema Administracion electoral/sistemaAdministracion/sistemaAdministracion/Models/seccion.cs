using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaAdministracion.Models
{
    public class Seccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Coordenadas { get; set; }

        public ICollection<SeccionEleccion> Elecciones { get; set; }
        public ICollection<CargoSeccion> Cargos { get; set; }
    }

}

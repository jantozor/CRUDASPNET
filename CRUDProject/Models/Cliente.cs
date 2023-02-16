using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDProject.Models
{
    public class Cliente
    {        
        public Cliente()
        {
            Ordenes = new HashSet<Orden>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Dirección")]
        public string? Address { get; set; }
        [Required]
        [Display(Name = "Correo")]
        public string? Email { get; set; }
        public virtual ICollection<Orden> Ordenes { get; set; }

    }
}

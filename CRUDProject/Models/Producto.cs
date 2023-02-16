using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDProject.Models
{
    public class Producto
    {
        public Producto()
        {
            OrdenItems = new HashSet<OrdenItem>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public double Price { get; set; }
        [Display(Name = "Descripción")]
        public string? Description { get; set; }
        public virtual ICollection<OrdenItem> OrdenItems { get; set; }

    }
}

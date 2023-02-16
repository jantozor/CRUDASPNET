using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDProject.Models
{
    public class OrdenItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Orden")]
        [Display(Name = "Orden")]
        public int OrdenId { get; set; }
        [Required]
        [ForeignKey("Producto")]
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        [Required]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
    }

    public class Orden
    {
        public Orden()
        {
            OrdenItems = new HashSet<OrdenItem>();
        }
        public int Id { get; set; }
        [Required]
        [ForeignKey("Cliente")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Total")]
        public double TotalPrice { get; set; }
        [Display(Name = "Descripción")]
        public string? Description { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<OrdenItem> OrdenItems { get; set; }


    }
}

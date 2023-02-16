using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CRUDProject.Models;

namespace CRUDProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CRUDProject.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<CRUDProject.Models.Producto> Producto { get; set; } = default!;
        public DbSet<CRUDProject.Models.Orden> Orden { get; set; } = default!;
        public DbSet<CRUDProject.Models.OrdenItem> OrdenItem { get; set; } = default!;

    }
}

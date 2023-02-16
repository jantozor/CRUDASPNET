using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDProject.Data;
using CRUDProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace CRUDProject.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public IndexModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Producto> Producto { get;set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? filter { get; set; }
        public async Task OnGetAsync()
        {
            var productos = from m in _context.Producto
                           select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                switch (filter)
                {
                    case "Id":
                        productos = productos.Where(s => s.Id.ToString().Contains(SearchString));
                        break;
                    case "Name":
                        productos = productos.Where(s => s.Name.Contains(SearchString));
                        break;
                    case "Price":
                        productos = productos.Where(s => s.Price.ToString().Contains(SearchString));
                        break;
                    case "Description":
                        productos = productos.Where(s => s.Description.Contains(SearchString));
                        break;
                    case "All":
                        productos = productos.Where(s => s.Description.Contains(SearchString) || s.Price.ToString().Contains(SearchString) || s.Name.Contains(SearchString) || s.Id.ToString().Contains(SearchString));
                        break;

                }
            }
            Producto = await productos.ToListAsync();
        }
    }
}

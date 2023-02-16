using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDProject.Data;
using CRUDProject.Models;

namespace CRUDProject.Pages.Ordenes
{
    public class IndexModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public IndexModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Orden> Orden { get;set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? filter { get; set; }
        public async Task OnGetAsync()
        {
            var ordenes = from m in _context.Orden
                            select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                switch (filter)
                {
                    case "Id":
                        ordenes = ordenes.Where(s => s.Id.ToString().Contains(SearchString));
                        break;
                    case "Client":
                        ordenes = ordenes.Where(s => s.Cliente.Name.Contains(SearchString));
                        break;
                    case "Price":
                        ordenes = ordenes.Where(s => s.TotalPrice.ToString().Contains(SearchString));
                        break;
                    case "Description":
                        ordenes = ordenes.Where(s => s.Description.Contains(SearchString));
                        break;
                    case "Date":
                        ordenes = ordenes.Where(s => s.OrderDate.ToString().Contains(SearchString));
                        break;
                    case "All":
                        ordenes = ordenes.Where(s => s.Description.Contains(SearchString) || s.TotalPrice.ToString().Contains(SearchString) || s.OrderDate.ToString().Contains(SearchString) || s.Cliente.Name.Contains(SearchString) || s.Id.ToString().Contains(SearchString));
                        break;

                }
            }
            Orden = await ordenes
                .Include(o => o.Cliente).ToListAsync();
        }
    }
}

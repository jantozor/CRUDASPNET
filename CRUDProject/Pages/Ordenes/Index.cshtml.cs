using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDProject.Data;
using CRUDProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CRUDProject.Pages.Ordenes
{
    [Authorize]
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

        [BindProperty]
        public Orden OrdenBP { get; set; }
        [BindProperty]
        public OrdenItem OrderItem { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            OrdenBP.TotalPrice = 0;
            _context.Orden.Add(OrdenBP);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task OnGetAsync()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Producto, "Id", "Name");

            Orden = await GetOrden()
                .Include(o => o.Cliente).ToListAsync();
        }

        private IQueryable<Orden> GetOrden()
        {
            IQueryable<Orden> ordenes = from m in _context.Orden
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

            return ordenes;
        }
    }
}

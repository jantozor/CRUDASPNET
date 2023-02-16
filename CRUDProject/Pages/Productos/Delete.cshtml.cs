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
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public DeleteModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Producto Producto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto = await _context.Producto.FirstOrDefaultAsync(m => m.Id == id);

            if (Producto == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto = await _context.Producto.FindAsync(id);

            if (Producto != null)
            {
                _context.Producto.Remove(Producto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

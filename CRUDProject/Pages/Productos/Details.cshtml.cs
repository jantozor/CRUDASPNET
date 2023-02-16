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
    public class DetailsModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public DetailsModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}

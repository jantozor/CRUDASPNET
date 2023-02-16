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
    public class DetailsModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public DetailsModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Orden Orden { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orden = await _context.Orden
                .Include(o => o.Cliente).FirstOrDefaultAsync(m => m.Id == id);

            if (Orden == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

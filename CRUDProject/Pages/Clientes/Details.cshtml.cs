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

namespace CRUDProject.Pages.Clientes
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public DetailsModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.Id == id);

            if (Cliente == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

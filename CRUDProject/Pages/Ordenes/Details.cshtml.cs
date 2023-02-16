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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDProject.Pages.Ordenes
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public DetailsModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Orden Orden { get; set; }
        public IList<OrdenItem> items { get; set; }

        //public IActionResult OnGet(int? id, string action)
        //{
        //    if (action == "Delete")
        //    {
        //        var item = _context.OrdenItem.Find(id);

        //        if (Orden != null)
        //        {
        //            _context.OrdenItem.Remove(item);
        //        }
        //    }
        //    return Page();
        //}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Name");

            Orden = await _context.Orden
                .Include(o => o.Cliente).Include(o => o.OrdenItems).FirstOrDefaultAsync(m => m.Id == id);

            if (Orden == null)
            {
                return NotFound();
            }

            items = await _context.OrdenItem
                .Include(o => o.Producto).ToListAsync();

            OrderItem = new OrdenItem();
            OrderItem.OrdenId = Orden.Id;

            return Page();
        }

        [BindProperty]
        public OrdenItem OrderItem { get; set; }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var it = await _context.OrdenItem.FindAsync(id);

            if (it != null)
            {
                _context.OrdenItem.Remove(it);
                await _context.SaveChangesAsync();
            }

            return Redirect("./Details?id=" + it.OrdenId.ToString());
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./index");
            }

            var Producto = await _context.Producto.FirstOrDefaultAsync(m => m.Id == OrderItem.ProductoId);
            OrderItem.Producto = Producto;

            _context.OrdenItem.Add(OrderItem);
            //Orden.OrdenItems.Add(OrderItem);
            await _context.SaveChangesAsync();

            return await OnGetAsync(OrderItem.OrdenId);
        }
    }
}

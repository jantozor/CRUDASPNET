using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDProject.Data;
using CRUDProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CRUDProject.Pages.Ordenes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public CreateModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Producto, "Id", "Name");

            //Orden = new Orden();
            OrderItem = new OrdenItem();
            return Page();
        }

        [BindProperty]
        public Orden Orden { get; set; }
        [BindProperty]
        public OrdenItem OrderItem { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            if(Orden.Cliente== null)
            {
                var Producto = await _context.Producto.FirstOrDefaultAsync(m => m.Id == OrderItem.ProductoId);
                OrderItem.Producto = Producto;

                //Orden.OrdenItems.Add(item);
                return OnGet();
            }


            double total = 0;

            foreach (var item in Orden.OrdenItems)
            {
                item.OrdenId = Orden.Id;
                total += item.Quantity * item.Producto.Price;
            }

            Orden.TotalPrice = total;

            _context.Orden.Add(Orden);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

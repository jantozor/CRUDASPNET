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

namespace CRUDProject.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly CRUDProject.Data.ApplicationDbContext _context;

        public IndexModel(CRUDProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Cliente> Cliente { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? filter { get; set; }
        //public SelectList? addresses { get; set; }
        //[BindProperty(SupportsGet = true)]
        //public string? address { get; set; }


        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            //IQueryable<string> genreQuery = from m in _context.Cliente
            //                                orderby m.Id
            //                                select m.Address;
            var clientes = from m in _context.Cliente
                           select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                switch (filter)
                {
                    case "Id":
                        clientes = clientes.Where(s => s.Id.ToString().Contains(SearchString));
                        break;
                    case "Name":
                        clientes = clientes.Where(s => s.Name.Contains(SearchString));
                        break;
                    case "Address":
                        clientes = clientes.Where(s => s.Address.Contains(SearchString));
                        break;
                    case "Email":
                        clientes = clientes.Where(s => s.Email.Contains(SearchString));
                        break;
                    case "All":
                        clientes = clientes.Where(s => s.Email.Contains(SearchString) || s.Address.Contains(SearchString) || s.Name.Contains(SearchString) || s.Id.ToString().Contains(SearchString));
                        break;

                }
            }

                //if (!string.IsNullOrEmpty(SearchString))
                //{
                //    clientes = clientes.Where(s => s.Name.Contains(SearchString));
                //}
                //if (!string.IsNullOrEmpty(address))
                //{
                //    clientes = clientes.Where(x => x.Address == address);
                //}
                //addresses = new SelectList(await genreQuery.Distinct().ToListAsync());
            Cliente = await clientes.ToListAsync();
        }
    }
}

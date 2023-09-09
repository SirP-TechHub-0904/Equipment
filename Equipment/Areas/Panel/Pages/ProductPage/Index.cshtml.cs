using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Equipment.Data;
using Equipment.Data.Model;

namespace Equipment.Areas.Panel.Pages.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly Equipment.Data.ApplicationDbContext _context;

        public IndexModel(Equipment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                
                .ToListAsync();
        }
    }
}

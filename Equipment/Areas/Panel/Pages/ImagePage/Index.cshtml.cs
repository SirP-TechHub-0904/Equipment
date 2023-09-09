using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Equipment.Data;
using Equipment.Data.Model;

namespace Equipment.Areas.Panel.Pages.ImagePage
{
    public class IndexModel : PageModel
    {
        private readonly Equipment.Data.ApplicationDbContext _context;

        public IndexModel(Equipment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Image> Image { get;set; }

        public async Task OnGetAsync()
        {
            Image = await _context.Images
                .Include(i => i.Product).ToListAsync();
        }
    }
}

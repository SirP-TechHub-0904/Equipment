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
    public class DetailsModel : PageModel
    {
        private readonly Equipment.Data.ApplicationDbContext _context;

        public DetailsModel(Equipment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Image Image { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Image = await _context.Images
                .Include(i => i.Product).FirstOrDefaultAsync(m => m.Id == id);

            if (Image == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

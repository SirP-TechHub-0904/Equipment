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
    public class DeleteModel : PageModel
    {
        private readonly Equipment.Data.ApplicationDbContext _context;

        public DeleteModel(Equipment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Image = await _context.Images.FindAsync(id);

            if (Image != null)
            {
                _context.Images.Remove(Image);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

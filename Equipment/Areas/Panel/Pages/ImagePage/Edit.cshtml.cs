using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Equipment.Data;
using Equipment.Data.Model;

namespace Equipment.Areas.Panel.Pages.ImagePage
{
    public class EditModel : PageModel
    {
        private readonly Equipment.Data.ApplicationDbContext _context;

        public EditModel(Equipment.Data.ApplicationDbContext context)
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
           ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(Image.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ImageExists(long id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}

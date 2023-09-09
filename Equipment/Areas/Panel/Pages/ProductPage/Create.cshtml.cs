using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equipment.Data;
using Equipment.Data.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Equipment.Data.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Amazon.S3.Model;
using Equipment.Model.AwsDto;

namespace Equipment.Areas.Panel.Pages.ProductPage
{
    public class CreateModel : PageModel
    {
        private readonly Equipment.Data.ApplicationDbContext _context;
        private IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public CreateModel(Equipment.Data.ApplicationDbContext context, IHostingEnvironment environment, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            return Page();
        }
        [BindProperty]
        public IFormFile file { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Process file
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileExt = Path.GetExtension(file.FileName);
            var docName = $"{Guid.NewGuid()}{fileExt}";
            // call server

            var s3Obj = new Model.AwsDto.S3Object()
            {
                BucketName = "equipment-xyz",
                InputStream = memoryStream,
                Name = docName
            };

            var cred = new AwsCredentials()
            {
                AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                SecretKey = _config["AwsConfiguration:AWSSecretKey"]
            };

            var result = await _storageService.UploadFileAsync(s3Obj, cred);
            // 

            string[] tokens = result.Message.Split(' ');
            string retVal = tokens[0];

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            Image img = new Image();
            img.Description = "First Image";
            img.ImageUrl = retVal;
            img.ProductId = Product.Id;
            _context.Images.Add(img);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

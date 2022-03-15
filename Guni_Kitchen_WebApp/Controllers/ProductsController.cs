using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Guni_Kitchen_WebApp.Data;
/*using Guni_Kitchen_WebApp.Models;
*/using Guni_Kitchen_WebApp.ViewModels;
using Guni_Kitchen_WebApp.Models;
using Azure.Storage.Blobs;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Guni_Kitchen_WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private const string BlobContainerNAME = "productimages";


        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;



        public ProductsController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Product.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Product_Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Category_Id", "Category_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            ([Bind("Product_Id,Product_Name,Product_Price,Product_image,Size,UnitOfMeasure,Photo,Product_Description,CategoryId")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                string photoUrl = null;
                if (productViewModel.Photo != null)
                {
                    photoUrl = await SavePhotoToBlobAsync(productViewModel.Photo);
                }

                Product newProduct = new Product
                {
                    Product_Id = productViewModel.Product_Id,
                    Product_Name = productViewModel.Product_Name,
                    Product_Description = productViewModel.Product_Description,
                    Product_Price = productViewModel.Product_Price,
                    UnitOfMeasure = productViewModel.UnitOfMeasure,
                    Size = productViewModel.Size,
                    CategoryId = productViewModel.CategoryId,
                    ProductImageFileUrl = photoUrl,
                    ProductImageContentType = photoUrl == null ? null : productViewModel.Photo.ContentType

                    /*                    Product_image=product.Product_image
                    */
                };
                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Category_Id", "Category_Name", productViewModel.CategoryId);
            return View(productViewModel);
        }

      

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productViewModel = new ProductViewModel
            {
                Product_Id = product.Product_Id,
                CategoryId = product.CategoryId,
                Product_Name = product.Product_Name,
                Product_Description = product.Product_Description,
                Product_Price = product.Product_Price,
                UnitOfMeasure = product.UnitOfMeasure,
                Size = product.Size
            };
            ViewBag.ProductImageFileUrl = product.ProductImageFileUrl;

            ViewData["CategoryId"] = new SelectList(_context.Category, "Category_Id", "Category_Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Product_Id,Product_Name,UnitOfMeasure,Product_Price,Product_image,Size,Product_Description,CategoryId")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.Product_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string photoUrl = null;
                if (productViewModel.Photo != null)
                {
                    // Upload the product image to the Blob Storage Account.
                    photoUrl = await SavePhotoToBlobAsync(productViewModel.Photo);
                }
                // Get the product to update, and update its properties.
                var product = _context.Product.SingleOrDefault(p => p.Product_Id == productViewModel.Product_Id);
                product.CategoryId = productViewModel.CategoryId;
                product.Product_Name = productViewModel.Product_Name;
                product.Product_Description = productViewModel.Product_Description;
                product.Product_Price = productViewModel.Product_Price;
                product.UnitOfMeasure = productViewModel.UnitOfMeasure;
                product.Size = productViewModel.Size;
                if (photoUrl != null)
                {
                    product.ProductImageFileUrl = photoUrl;
                    product.ProductImageContentType = productViewModel.Photo.ContentType;
                }

                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Product_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Category_Id", "Category_Name", productViewModel.CategoryId);
            return View(productViewModel);
        }

        private Task<string> SavePhotoToBlobAsync(object photo)
        {
            throw new NotImplementedException();
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Product_Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Product_Id == id);
        }
        private async Task<string> SavePhotoToBlobAsync(IFormFile productImage)
        {
            string storageConnection1 = _config.GetValue<string>("MyAzureSettings:StorageAccountKey1");
            string storageConnection2 = _config.GetValue<string>("MyAzureSettings:StorageAccountKey2");
            string fileName = productImage.FileName;
            string tempFilePath = string.Empty;
            string photoUrl;

            if (productImage != null && productImage.Length > 0)
            {
                // Save the uploaded file on to a TEMP file.
                tempFilePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(tempFilePath))
                {
                    productImage.CopyToAsync(stream).Wait();
                }
            }

            // Get a reference to a container 
            BlobContainerClient blobContainerClient = new BlobContainerClient(storageConnection1, BlobContainerNAME);

            // Create the container if it does not exist - granting PUBLIC access.
            await blobContainerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            // Create the client to the Blob Item
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            // Open the file and upload its data
            using (FileStream uploadFileStream = System.IO.File.OpenRead(tempFilePath))
            {
                await blobClient.UploadAsync(uploadFileStream, overwrite: true);
                uploadFileStream.Close();
            }

            // Delete the TEMP file since it is no longer needed.
            System.IO.File.Delete(tempFilePath);

            // Return the URI of the item in the Blob Storage
            photoUrl = blobClient.Uri.ToString();
            return photoUrl;
        }
        }
}

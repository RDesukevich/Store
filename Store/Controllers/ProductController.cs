using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.Get());
        }

        public async Task<IActionResult> Create(Guid productId)
        {
            var r = (await _productService.Get()).ToList();
            ViewBag.categoryId = new SelectList(await _categoryService.Get(), "Id", "Name");
            return View(await _productService.GetAsync(productId));
        }

        public async Task<IActionResult> Edit(Guid productId)
        {
            var r = (await _productService.Get()).ToList();
            ViewBag.categoryId = new SelectList(await _categoryService.Get(), "Id", "Name");
            return View(await _productService.GetAsync(productId));
        }

        public async Task<IActionResult> Delete(Guid productId)
        {
            return View(await _productService.GetAsync(productId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            string upload = webRootPath + WC.ImagePath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            product.Image = fileName+extension;
            var productId = await _productService.CreateAsync(product);
            return RedirectToAction("Index", "Product", new {productId});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var files = HttpContext.Request.Form.Files;
            var objFromDb = _productService.GetEdit(product);
            if (files.Count > 0)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string upload = webRootPath + WC.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                var oldFile = Path.Combine(upload, objFromDb.Result.Image);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                product.Image = fileName + extension;
            }
            else
            {
                product.Image = objFromDb.Result.Image;
            }

            var productId = await _productService.EditAsync(product);
            return RedirectToAction("Index", "Product", new {productId});
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(Guid productId)
        {
            var obj = _productService.GetDelete(productId);
            string webRootPath = _webHostEnvironment.WebRootPath;
            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.Result.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }
            await _productService.DeleteAsync(productId);
            return RedirectToAction("Index");
        }
    }
}

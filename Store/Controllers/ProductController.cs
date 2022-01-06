using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.Get());
        }

        public async Task<IActionResult> Details(Guid productId)
        {
            return View(await _productService.GetAsync(productId));
        }

        public async Task<IActionResult> Create(Guid productId)
        {
            return View(await _productService.GetAsync(productId));
        }

        public async Task<IActionResult> Edit(Guid productId)
        {
            return View(await _productService.GetAsync(productId));
        }

        public async Task<IActionResult> Delete(Guid productId)
        {
            return View(await _productService.GetAsync(productId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var productId = await _productService.CreateAsync(product);
            return RedirectToAction("Index", "Product", new {productId});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var productId = await _productService.EditAsync(product);
            return RedirectToAction("Index", "Product", new {productId});
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(Guid productId)
        {
            await _productService.DeleteAsync(productId);
            return RedirectToAction("Index");
        }
    }
}

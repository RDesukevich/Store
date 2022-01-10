using System;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Services;

namespace Store.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeService _applicationTypeService;
        private readonly IProductService _productService;

        public ApplicationTypeController(IApplicationTypeService applicationTypeService, IProductService productService)
        {
            _applicationTypeService = applicationTypeService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _applicationTypeService.Get());
        }

        public async Task<IActionResult> Create(Guid applicationTypeId)
        {
            return View(await _applicationTypeService.GetAsync(applicationTypeId));
        }

        public async Task<IActionResult> Edit(Guid applicationTypeId)
        {
            return View(await _applicationTypeService.GetAsync(applicationTypeId));
        }

        public async Task<IActionResult> Delete(Guid applicationTypeId)
        {
            ViewBag.applicationTypeId = new SelectList(await _applicationTypeService.Get(), "Id", "Name");
            return View(await _applicationTypeService.GetAsync(applicationTypeId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationType applicationType)
        {
            var applicationTypeId = await _applicationTypeService.CreateAsync(applicationType);
            return RedirectToAction("Index", "ApplicationType", new {applicationTypeId});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationType applicationType)
        {
            var applicationTypeId = await _applicationTypeService.EditAsync(applicationType);
            return RedirectToAction("Index", "ApplicationType", new {applicationTypeId});
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(Guid applicationTypeId)
        {
            await _applicationTypeService.DeleteAsync(applicationTypeId);
            return RedirectToAction("Index");
        }
    }
}

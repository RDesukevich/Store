﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Data;
using Store.Models;
using Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryServise;

        public CategoryController(ICategoryService categoryServise)
        {
            _categoryServise = categoryServise;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _categoryServise.Get());
        }

        public async Task<IActionResult> Details(Guid categoryId)
        {
            return View(await _categoryServise.GetAsync(categoryId));
        }
        public async Task<IActionResult> Create(Guid categoryId)
        {
            return View( await _categoryServise.GetAsync(categoryId));
        }

        public async Task<IActionResult> Edit(Guid categoryId)
        {
            return View(await _categoryServise.GetAsync(categoryId));
        }

        public async Task<IActionResult> Delete(Guid categoryId)
        {
            return View(await _categoryServise.GetAsync(categoryId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var categoryId = await _categoryServise.CreateAsync(category);
            return RedirectToAction("Index", "Category", new {categoryId});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var categoryId = await _categoryServise.EditAsync(category);
            return RedirectToAction("Index", new { categoryId });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(Guid categoryId)
        {
            await _categoryServise.DeleteAsync(categoryId);
            return RedirectToAction("Index");
        }
    }
}

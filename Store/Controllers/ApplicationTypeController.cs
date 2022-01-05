using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;
using System.Collections.Generic;

namespace Store.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly AppDbContext _db;

        public ApplicationTypeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> applicationType = _db.ApplicationType;
            return View(applicationType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApplicationType applicationType)
        {
            _db.ApplicationType.Add(applicationType);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

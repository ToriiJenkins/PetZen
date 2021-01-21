using Microsoft.AspNet.Identity;
using PetZen.Data;
using PetZen.Models.AdministrationModels;
using PetZen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetZen.WebMVC.Controllers
{
    public class AdministrationController : Controller
    {
        //Add link to database: Application DB context
        private ApplicationDbContext _db = new ApplicationDbContext();

        //GET: Feeding
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AdministrationService(userId);
            var model = service.GetAdministrations();
            return View(model);
        }

        //GET: Administration/CREATE
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new AdministrationCreate();

            viewModel.Pets = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });

            viewModel.Medications = _db.Medications.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.MedId.ToString()
            });

            return View(viewModel);

        }

        //POST: AdministrationCreate
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdministrationCreate model)
        {
            ViewData["Pets"] = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });

            ViewData["Medications"] = _db.Medications.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.MedId.ToString()
            });

            if (!ModelState.IsValid) return View(model);

            var service = CreateAdministrationService();

            if (service.CreateAdministration(model))
            {
                TempData["SaveResult"] = "Your administration has been added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Administration could not be added.");

            return View(model);
        }

        private AdministrationService CreateAdministrationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AdministrationService(userId);
            return service;
        }
    }
}
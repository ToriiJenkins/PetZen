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

        //POST: Administration/Create
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

        //GET: Administration/Detail
        [Authorize]
        public ActionResult Details(int id)
        {
            var svc = CreateAdministrationService();
            var model = svc.GetAdministrationById(id);

            return View(model);
        }

        //GET: Administration/Edit
        [Authorize]
        public ActionResult Edit(int id)
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

            var service = CreateAdministrationService();
            var detail = service.GetAdministrationById(id);
            var model =
                new AdministrationEdit
                {
                    AdminId = detail.AdminId,
                    //PetId = detail.PetId,
                    PetName = detail.PetName,
                   // MedId = detail.MedId,
                    MedName = detail.MedName,
                    AdminDateTime = detail.AdminDateTime,
                    Dosage = detail.Dosage,
                    DoseMeasure = detail.DoseMeasure,
                    Default = detail.Default,
                    Notes = detail.Notes
                };

            return View(model);
        }

        //POST: Administration/Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AdministrationEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AdminId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateAdministrationService();

            if (service.UpdateAdministration(model))
            {
                TempData["SaveResult"] = "Your administration has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your administration could not be updated.");

            return View(model);
        }

        //GET: Administration/Delete
        [Authorize]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAdministrationService();
            var model = svc.GetAdministrationById(id);

            return View(model);
        }

        //POST: Administration/Delete
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult AdministrationFeeding(int id)
        {
            var service = CreateAdministrationService();

            service.DeleteAdministration(id);

            TempData["SaveResult"] = "Your administration has been removed.";

            return RedirectToAction("Index");
        }


        private AdministrationService CreateAdministrationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AdministrationService(userId);
            return service;
        }
    }
}
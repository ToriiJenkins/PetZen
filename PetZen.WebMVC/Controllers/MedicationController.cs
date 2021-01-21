using Microsoft.AspNet.Identity;
using PetZen.Data;
using PetZen.Models;
using PetZen.Models.MedicationModels;
using PetZen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetZen.WebMVC.Controllers
{
    public class MedicationController : Controller
    {
        //Add link to database: Application DB context
        private ApplicationDbContext _db = new ApplicationDbContext();


        // GET: Medication
        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MedicationService(userId);
            var model = service.GetMedications();
            return View(model);
        }

        //GET: Medication/CREATE
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new MedicationCreate();

            //viewModel.Pets = _db.Pets.Select(p => new SelectListItem
            //{
            //    Text = p.Name,
            //    Value = p.PetId.ToString()
            //});

            return View(viewModel);

        }

        //POST: Medication/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicationCreate model)
        {
            //ViewData["Pets"] = _db.Pets.Select(p => new SelectListItem
            //{
            //    Text = p.Name,
            //    Value = p.PetId.ToString()
            //});

            if (!ModelState.IsValid) return View(model);

            var service = CreateMedicationService();

            if (service.CreateMedication(model))
            {
                TempData["SaveResult"] = "Your medication has been added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Medication could not be added.");

            return View(model);
        }

        //GET: Medication/Detail
        [Authorize]
        public ActionResult Details(int id)
        {
            var svc = CreateMedicationService();
            var model = svc.GetMedicationById(id);

            ViewData["Pets"] = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });
           
            return View(model);
        }

        //GET: Medications/Edit
        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewData["Pets"] = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });
            var service = CreateMedicationService();
            var detail = service.GetMedicationById(id);
            var model =
                new MedicationEdit
                {
                    MedId = detail.MedId,
                    Name = detail.Name,
                   /* PetId = detail.PetId,  */                /* Dosage = detail.Dosage,*/
                    BeginDate = detail.BeginDate,
                    EndDate = detail.EndDate,
                    TimesPerDay = detail.TimesPerDay,
                    RefillLink = detail.RefillLink
                };
            return View(model);
        }

        //POST: Medication/Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MedicationEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.MedId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateMedicationService();

            if (service.UpdateMedication(model))
            {
                TempData["SaveResult"] = "Your medication has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your medication could not be updated.");
            return View(model);
        }

        //GET: Medication/Delete
        [Authorize]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateMedicationService();
            var model = svc.GetMedicationById(id);

            return View(model);
        }

        //POST: Medication/Delete
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMedication(int id)
        {
            var service = CreateMedicationService();

            service.DeleteMedication(id);

            TempData["SaveResult"] = "Your medication has been removed.";

            return RedirectToAction("Index");
        }

        private MedicationService CreateMedicationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MedicationService(userId);
            return service;
        }


    }
}
using Microsoft.AspNet.Identity;
using PetZen.Data;
using PetZen.Models.FeedingModels;
using PetZen.Models.FoodModels;
using PetZen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetZen.WebMVC.Controllers
{
    public class FeedingController : Controller
    {

        //Add link to database: Application DB context
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Feeding
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FeedingService(userId);
            var model = service.GetFeedings();
            return View(model);
        }

        //GET: Feeding/CREATE
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new FeedingCreate();

            viewModel.Pets = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });

            viewModel.Foods = _db.Foods.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.FoodId.ToString()
            });

            return View(viewModel);

        }

        //POST: FeedingCreate
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedingCreate model)
        {
            ViewData["Pets"] = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });

            ViewData["Foods"] = _db.Foods.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.FoodId.ToString()
            });

            if (!ModelState.IsValid) return View(model);

            var service = CreateFeedingService();

            if (service.CreateFeeding(model))
            {
                TempData["SaveResult"] = "Your Feeding has been added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Feeding could not be added.");

            return View(model);
        }

        //GET: Feeding/Detail
        [Authorize]
        public ActionResult Details(int id)
        {
            var svc = CreateFeedingService();
            var model = svc.GetFeedingById(id);

            return View(model);
        }

        //GET: Feeding/Edit
        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewData["Pets"] = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });

            ViewData["Foods"] = _db.Foods.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.FoodId.ToString()
            });

            var service = CreateFeedingService();
            var detail = service.GetFeedingById(id);
            var model =
                new FeedingEdit
                {
                    FeedingId = detail.FeedingId,
                    FeedDateTime = detail.FeedDateTime,
                    PetId = detail.PetId,
                    FoodId = detail.FoodId,
                    AmountFed = detail.AmountFed,
                    Measurement = detail.Measurement,
                    Default =detail.Default,
                    Notes = detail.Notes
                };

            return View(model);
        }

        //POST: Feeding/Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FeedingEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.FeedingId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateFeedingService();

            if (service.UpdateFeeding(model))
            {
                TempData["SaveResult"] = "Your feeding has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your feeding could not be updated.");

            return View(model);
        }

        

        //GET: Feeding/Delete
        [Authorize]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateFeedingService();
            var model = svc.GetFeedingById(id);

            return View(model);
        }

        //POST: Feeding/Delete
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFeeding(int id)
        {
            var service = CreateFeedingService();

            service.DeleteFeeding(id);

            TempData["SaveResult"] = "Your feeding has been removed.";

            return RedirectToAction("Index");
        }

        private FeedingService CreateFeedingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FeedingService(userId);
            return service;
        }
    }
}
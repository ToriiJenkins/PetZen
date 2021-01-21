using Microsoft.AspNet.Identity;
using PetZen.Data;
using PetZen.Models;
using PetZen.Models.ActivityModels;
using PetZen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetZen.WebMVC.Controllers
{
    public class ActivityController : Controller
    {
        //Add link to database: Application DB context
        private ApplicationDbContext _db = new ApplicationDbContext();


        // GET: Activity
        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ActivityService(userId);
            var model = service.GetActivities();
            return View(model);
        }

        //GET: Activity/CREATE
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new ActivityCreate();

            viewModel.Pets = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });

            return View(viewModel);

        }

        //POST: Activity/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityCreate model)
        {
            ViewData["Pets"] = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });

            if (!ModelState.IsValid) return View(model);

            var service = CreateActivityService();

            if (service.CreateActivity(model))
            {
                TempData["SaveResult"] = "Your activity has been added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Activity could not be added.");

            return View(model);
        }

        //GET: Activity/Detail
        [Authorize]
        public ActionResult Details(int id)
        {
            var svc = CreateActivityService();
            var model = svc.GetActivityById(id);

            return View(model);
        }

        //GET: Activity/Edit
        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewData["Pets"] = _db.Pets.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PetId.ToString()
            });

            var service = CreateActivityService();
            var detail = service.GetActivityById(id);
            var model =
                new ActivityEdit
                {
                    ActivityId = detail.ActivityId,
                    ActType = detail.ActType,
                    PetId = detail.PetId,
                    Date = detail.Date, 
                    Notes = detail.Notes
                };

            return View(model);
        }

        //POST: Activity/Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ActivityEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ActivityId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateActivityService();

            if (service.UpdateActivity(model))
            {
                TempData["SaveResult"] = "Your activity has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your activity could not be updated.");
            return View(model);
        }

        //GET: Activity/Delete
        [Authorize]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateActivityService();
            var model = svc.GetActivityById(id);

            return View(model);
        }

        //POST: Activity/Delete
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteActivity(int id)
        {
            var service = CreateActivityService();

            service.DeleteActivity(id);

            TempData["SaveResult"] = "Your activity has been removed.";

            return RedirectToAction("Index");
        }

        private ActivityService CreateActivityService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ActivityService(userId);
            return service;
        }
    }
}
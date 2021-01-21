using Microsoft.AspNet.Identity;
using PetZen.Models.FoodModels;
using PetZen.Services;
using System;
using System.Web.Mvc;

namespace PetZen.WebMVC.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food
        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FoodService(userId);
            var model = service.GetFoods();
            return View(model);
        }

        //GET: Food/CREATE
        [Authorize]
        public ActionResult Create()
        {
            return View();

        }

        //POST: Food/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateFoodService();

            if (service.CreateFood(model))
            {
                TempData["SaveResult"] = "Your food has been added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "food could not be added.");

            return View(model);
        }

        //GET: Food/Detail
        [Authorize]
        public ActionResult Details(int id)
        {
            var svc = CreateFoodService();
            var model = svc.GetFoodById(id);

            return View(model);
        }

        //GET: Food/Edit
        [Authorize]
        public ActionResult Edit(int id)
        {
            var service = CreateFoodService();
            var detail = service.GetFoodById(id);
            var model =
                new FoodEdit
                {
                    FoodId = detail.FoodId,
                    Name = detail.Name,
                    Species = detail.Species,
                    //ServingSize = detail.ServingSize,
                    PurchaseLink = detail.PurchaseLink

                };
            return View(model);
        }

        //POST: Food/Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FoodEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.FoodId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateFoodService();

            if (service.UpdateFood(model))
            {
                TempData["SaveResult"] = "Your food was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your food could not be updated.");
            return View(model);
        }

        //GET: Food/Delete
        [Authorize]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateFoodService();
            var model = svc.GetFoodById(id);

            return View(model);
        }

        //POST: Food/Delete
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFood(int id)
        {
            var service = CreateFoodService();

            service.DeleteFood(id);

            TempData["SaveResult"] = "Your food has been removed.";

            return RedirectToAction("Index");
        }

        private FoodService CreateFoodService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FoodService(userId);
            return service;
        }
    }
}
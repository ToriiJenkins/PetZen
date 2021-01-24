using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetZen.WebMVC.Controllers
{
    public class DailyController : Controller
    {
        // GET: Daily
        public ActionResult Index()
        {
            return View();
        }
    }
}
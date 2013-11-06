using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHangar.Web.UI.Models;

namespace TestHangar.Web.UI.Controllers
{
    public class FeaturesController : Controller
    {
        public ActionResult Index(string id)
        {
            var repo = new ViewModelStore();
            var model = repo.GetFeatures(id);
            ViewBag.Id = id;
            return View(model);
        }

        public ActionResult View(string id)
        {
            var repo = new ViewModelStore();
            var model = repo.GetFeature(id);
            return View(model);
        }

        public ActionResult ByTag(string id)
        {
            var repo = new ViewModelStore();
            var model = repo.GetScenariosByTag(id);
            return View(model);
        }
    }
}

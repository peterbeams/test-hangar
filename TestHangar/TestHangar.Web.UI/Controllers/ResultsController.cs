﻿using System.Web.Mvc;
using TestHangar.Web.UI.Models;

namespace TestHangar.Web.UI.Controllers
{
    public class ResultsController : Controller
    {
        public ActionResult Index()
        {
            var repo = new ViewModelStore();
            var model = repo.GetResults();
            return View(model);
        }

        public ActionResult View(string id)
        {
            var repo = new ViewModelStore();
            var model = repo.GetResult();
            return View(model);
        }
    }
}
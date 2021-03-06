﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHangar.TestSuites;
using TestHangar.Web.UI.Models;

namespace TestHangar.Web.UI.Controllers
{
    public class TestLibrariesController : Controller
    {
        public ActionResult Index()
        {
            var repo = new ViewModelStore();
            var model = repo.GetTestLibraries();
            return View(model);
        }

        public ActionResult Selector()
        {
            var repo = new ViewModelStore();
            var model = repo.GetTestLibraries();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateTestSuiteViewModel model)
        {
            var reader = new TestSuiteImporter();
            reader.Import(model.Id, model.Path);
            ViewBag.SuccessMessage = "Test Suite Import Successful";

            var repo = new ViewModelStore();
            var viewModel = repo.GetTestLibraries();
            return View("Index", viewModel);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHangar.Cucumber;
using TestHangar.Databases;
using TestHangar.Model;
using TestHangar.Web.UI.Models;

namespace TestHangar.Web.UI.Controllers
{
    public class SuitesController : Controller
    {
        //
        // GET: /Suites/

        public ActionResult Index()
        {
            var repo = new ViewModelStore();
            var model = repo.GetTestSuites();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TestSuiteViewModel model)
        {
            var raven = new RavenClient();
            raven.Store(new TestSuite
                {
                    Id = model.Id,
                    Name = model.Name,
                    Tags = model.Tags.Split(',').Select(m => m.Trim()).ToArray(),
                    Library = model.Library
                });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Run(string id, string configuration)
        {
            var repo = new ViewModelStore();
            var model = repo.GetTestSuite(id);
            var library = repo.GetTestLibrary(model.Library);
            var runner = new CucumberRunner();
            runner.Start(configuration,  model.Tags, library.Path);

            return View();
        }
    }
}

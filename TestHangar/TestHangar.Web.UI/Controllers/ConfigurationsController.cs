using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestHangar.Databases;
using TestHangar.Model;
using TestHangar.Web.UI.Models;

namespace TestHangar.Web.UI.Controllers
{
    public class ConfigurationsController : Controller
    {
        public ActionResult Index()
        {
            var repo = new ViewModelStore();
            var model = repo.GetConfigurations();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateConfigurationViewModel model)
        {
            var client = new RavenClient();
            client.Store(new Configuration
                {
                    Id = model.Id
                });
            
            ViewBag.SuccessMessage = "Create Configuration Successful";
            var repo = new ViewModelStore();
            var viewModel = repo.GetConfigurations();

            return View("Index", viewModel);
        }

        public ActionResult Selector()
        {
            var repo = new ViewModelStore();
            var model = repo.GetConfigurations();
            return View(model);
        }
    }
}

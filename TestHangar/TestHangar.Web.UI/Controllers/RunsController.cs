using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TestHangar.Cucumber;
using TestHangar.Databases;

namespace TestHangar.Web.UI.Controllers
{
    public class RunsController : Controller
    {
        //
        // GET: /Run/

        public ActionResult Index()
        {
            return View();
        }

    }
}

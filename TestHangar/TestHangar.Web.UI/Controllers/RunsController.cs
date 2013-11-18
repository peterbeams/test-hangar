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

        [HttpPost]
        public ActionResult Start()
        {
            var startInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = @"C:\ruby193\bin\cucumber.bat",
                    Arguments = "environment=rpbeta --tags @pre_release_3 --format json",
                    WorkingDirectory = @"C:\Source\oms_acceptance_tests"
                };

            var id = Guid.NewGuid().ToString();

            var process = new Process { StartInfo = startInfo };
            var outputPath = string.Format(@"C:\data\out-{0}.json", id);

            process.OutputDataReceived += (sender, args) => System.IO.File.AppendAllText(outputPath, args.Data);
            process.ErrorDataReceived += (sender, args) => System.IO.File.AppendAllText(@"C:\data\error.txt", args.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            Thread.Sleep(5000);
            var reader = new RunResultsReader();
            var raven = new RavenClient();
            raven.Store(reader.Load(outputPath));

            return RedirectToAction("Index", "Results");
        }
    }
}

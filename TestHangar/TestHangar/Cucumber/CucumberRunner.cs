using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using TestHangar.Databases;

namespace TestHangar.Cucumber
{
    public class CucumberRunner
    {
        public void Start(string environment, string[] tags, string suitePath)
        {
            var startInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = @"C:\ruby193\bin\cucumber.bat",
                    Arguments = string.Format("environment={0} --tags {1} --format json", environment, string.Join(",", tags)),
                    WorkingDirectory = suitePath
                };

            var process = new Process { StartInfo = startInfo };
            
            var outputStream = new StringBuilder();
            var errorStream = new StringBuilder();

            process.OutputDataReceived += (sender, args) => outputStream.Append(args.Data);
            process.ErrorDataReceived += (sender, args) => errorStream.Append(args.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            Thread.Sleep(5000);
            var reader = new RunResultsReader();
            var raven = new RavenClient();
            var runResult = reader.Load(outputStream.ToString());
            raven.Store(runResult);
        }
    }
}
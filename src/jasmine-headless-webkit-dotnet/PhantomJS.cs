using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace jasmine_headless_webkit_dotnet
{
    public abstract class PhantomJS : IPhantomJS
    {
        private readonly string phantomFileLocation;
        protected readonly string jasmineTestFileLocation;
        private readonly VerbosityLevel verbosityLevel;
        private readonly int timeOut;
        public int NumberOfTests { get; private set; }
        public int NumberOfFailures { get; private set; }
        public int NumberOfSuccesses { get; private set; }

        protected PhantomJS(string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut)
        {
            this.phantomFileLocation = phantomFileLocation;
            this.jasmineTestFileLocation = jasmineTestFileLocation;
            this.verbosityLevel = verbosityLevel;
            this.timeOut = timeOut;
        }

        public bool Run()
        {
            try
            {
                var phantomProcess = CreatePhantomProcess();
                var output = StartProcessAndGetOutput(phantomProcess);
                if (!WaitProcessToComplete(phantomProcess)) return false;
                var testsPassed = VerifyTestRunFromOutput(output.ToString());
                return testsPassed && phantomProcess.ExitCode == 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception when running phantom:\n{0}", exception);
                return false;
            }
        }

        private bool WaitProcessToComplete(Process phantomProcess)
        {
            var exited = phantomProcess.WaitForExit(timeOut*1000);
            if (!exited)
            {
                phantomProcess.Kill();
                return false;
            }
            return true;
        }

        private static StringBuilder StartProcessAndGetOutput(Process phantomProcess)
        {
            var output = new StringBuilder();
            phantomProcess.OutputDataReceived += (sender, args) =>
                {
                    Console.WriteLine(args.Data);
                    Debug.WriteLine(args.Data);
                    output.AppendLine(args.Data);
                };
            phantomProcess.Start();
            phantomProcess.BeginOutputReadLine();
            return output;
        }

        private Process CreatePhantomProcess()
        {
            var phantomArgs = BuildArgs();
            var phantomProcess = new Process
                {
                    StartInfo = new ProcessStartInfo(phantomFileLocation, phantomArgs)
                        {RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true}
                };
            Debug.WriteLine("Starting process: {0} {1}", phantomProcess.StartInfo.FileName, phantomProcess.StartInfo.Arguments);
            if (verbosityLevel > VerbosityLevel.Verbose)
            {
                Console.WriteLine("Starting process: {0} {1}", phantomProcess.StartInfo.FileName,
                                  phantomProcess.StartInfo.Arguments);
            }
            return phantomProcess;
        }

        private bool VerifyTestRunFromOutput(string output)
        {
            //parse, format is "1 spec, 0 failures in 0.004s."
            var match = Regex.Match(output, @"(\d+) spec[s]*, (\d+) failure[s]*");
            NumberOfTests = Convert.ToInt32(match.Groups[1].Value);
            NumberOfFailures = Convert.ToInt32(match.Groups[2].Value);
            NumberOfSuccesses = NumberOfTests - NumberOfFailures;
            return !output.Contains("SyntaxError:") && NumberOfFailures == 0;
        }

        protected abstract string BuildArgs();

    }

    public interface IPhantomJS
    {
        bool Run();
        int NumberOfSuccesses { get; }
        int NumberOfFailures { get; }
    }
}
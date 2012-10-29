using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace jasmine_headless_webkit_dotnet
{
    public abstract class PhantomJSRunStragegy
    {
        private readonly string phantomFileLocation;
        protected readonly string jasmineTestFileLocation;
        private readonly VerbosityLevel verbosityLevel;
        private readonly int timeOut;

        protected PhantomJSRunStragegy(string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut)
        {
            this.phantomFileLocation = phantomFileLocation;
            this.jasmineTestFileLocation = jasmineTestFileLocation;
            this.verbosityLevel = verbosityLevel;
            this.timeOut = timeOut;
        }

        public int NumberOfSuccesses { get; private set; }

        public bool Run()
        {
            try
            {
                var phantomArgs = BuildArgs();
                var output = new StringBuilder();
                var phantomProcess = new Process
                {
                    StartInfo = new ProcessStartInfo(phantomFileLocation, phantomArgs)
                        {RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true}
                };
                phantomProcess.OutputDataReceived += (sender, args) =>
                    {
                        Console.WriteLine(args.Data);
                        Debug.WriteLine(args.Data);
                        output.AppendLine(args.Data);
                    };
                Debug.WriteLine("Starting process: {0} {1}", phantomProcess.StartInfo.FileName, phantomProcess.StartInfo.Arguments);
                if (verbosityLevel > VerbosityLevel.Verbose)
                {
                    Console.WriteLine("Starting process: {0} {1}", phantomProcess.StartInfo.FileName, phantomProcess.StartInfo.Arguments);
                }
                phantomProcess.Start();
                phantomProcess.BeginOutputReadLine();
                var exited = phantomProcess.WaitForExit(timeOut * 1000);
                if (!exited)
                {
                    phantomProcess.Kill();
                    return false;
                }
                var testsPassed = VerifyOutput(output.ToString());
                return testsPassed && phantomProcess.ExitCode == 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception when running phantom:\n{0}", exception);
                return false;
            }
        }

        private bool VerifyOutput(string output)
        {
            //parse, format is "1 spec, 0 failures in 0.004s."
            var match = Regex.Match(output, @"(\d+) spec[s]*, (\d+) failure[s]*");
            NumberOfTests = Convert.ToInt32(match.Groups[1].Value);
            NumberOfFailures = Convert.ToInt32(match.Groups[2].Value);
            NumberOfSuccesses = NumberOfTests - NumberOfFailures;
            return !output.Contains("SyntaxError:") && NumberOfFailures == 0;
        }

        protected int NumberOfTests { get; private set; }

        public int NumberOfFailures { get; private set; }

        public abstract string BuildArgs();
    }
}
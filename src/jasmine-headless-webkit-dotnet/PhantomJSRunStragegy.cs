using System;
using System.Diagnostics;

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

        public bool Run()
        {
            try
            {
                var phantomArgs = BuildArgs();
                
                var phantomProcess = new Process
                {
                    StartInfo = new ProcessStartInfo(
                        phantomFileLocation,
                        phantomArgs)
                };
                Debug.WriteLine("Starting process: {0} {1}", phantomProcess.StartInfo.FileName, phantomProcess.StartInfo.Arguments);
                if (verbosityLevel > VerbosityLevel.Verbose)
                {
                    Console.WriteLine("Starting process: {0} {1}", phantomProcess.StartInfo.FileName, phantomProcess.StartInfo.Arguments);
                }
                phantomProcess.Start();
                var exited = phantomProcess.WaitForExit(timeOut * 1000);
                if (!exited)
                {
                    phantomProcess.Kill();
                    return false;
                }
                if (phantomProcess.ExitCode != 0)
                    return false;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception when running phantom:\n{0}", exception);
                return false;
            }
            return true;
        }

        public abstract string BuildArgs();
    }
}
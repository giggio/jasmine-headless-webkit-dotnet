using System;
using System.Diagnostics;
using System.IO;

namespace jasmine_headless_webkit_dotnet
{
    public class PhantomJS : IPhantomJS
    {
        private readonly string phantomFileLocation;
        private readonly bool runningInServer;
        private readonly VerbosityLevel verbosityLevel;
        private readonly int timeOut;
        private readonly int port;
        private readonly string directory;
        private readonly string fileName;
        private readonly string jasmineTestFileLocation;

        public PhantomJS(ILocalEnvironment environment, bool runningInServer, VerbosityLevel verbosityLevel, int timeOut, int port, string directory, string fileName)
        {
            this.runningInServer = runningInServer;
            this.verbosityLevel = verbosityLevel;
            this.timeOut = timeOut;
            this.port = port;
            this.directory = directory;
            this.fileName = fileName;
            jasmineTestFileLocation = environment.GetRunJasmineTestFileLocation();
            phantomFileLocation = environment.GetPhantomFileLocation();
        }

        public bool Run()
        {
            try
            {
                //phantomjs.exe run_jasmine_test.coffee http://localhost:14815/testes.html
                //phantomjs.exe run_jasmine_test.coffee c:\testes\testes.html
                string phantomArgs;
                if (runningInServer)
                {
                    phantomArgs = string.Format("{0} http://localhost:{1}/{2}", jasmineTestFileLocation, port, fileName);
                }
                else
                {
                    var fullFileName = Path.Combine(directory, fileName).Replace('\\', '/');
                    phantomArgs = string.Format("{0} file:///{1}", jasmineTestFileLocation, fullFileName);
                }
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

    }

    public interface IPhantomJS
    {
        bool Run();
    }
}
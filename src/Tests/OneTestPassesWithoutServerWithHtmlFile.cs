using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests
{
    [TestClass]
    public class OneTestPassesWithoutServerWithHtmlFile
    {
        private bool runSucceeded;

        [TestInitialize]
        public void RunFiles()
        {
            var jasmineTestDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests"); 
            var args = new Arguments
                {
                    RunServer = false,
                    VerbosityLevel = VerbosityLevel.Verbose,
                    Timeout = 10,
                    Directory = jasmineTestDir,
                    FileName = "OneSpecPass1.1.0.html"
                };
            var environment = new LocalEnvironment();
            var test = new Test(
                new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(), args.Directory, args.FileName),
                new WebServer(),
                new Tools(environment),
                args);
            runSucceeded = test.Run();
        }

        [TestMethod]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }
    }
}

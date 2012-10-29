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
        private static bool runSucceeded;

        [ClassInitialize]
        public static void RunFiles(TestContext testContext)
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
            using (var webServer = new WebServer(args.RunServer, args.Directory, args.GetPort()))
            {
                var test = new Test(
                    new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(),
                                  args.Directory, args.FileName),
                    webServer,
                    new Tools(environment));
                runSucceeded = test.Run();
            }
        }

        [TestMethod]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }
    }
}

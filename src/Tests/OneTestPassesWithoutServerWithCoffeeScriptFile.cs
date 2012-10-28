using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests
{
    [TestClass, Ignore]
    public class OneTestPassesWithoutServerWithCoffeeScriptFile
    {
        private bool runSucceeded;

        [TestInitialize]
        public void RunFiles()
        {
            var sourceFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "Scripts", "calculator.coffee");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "ScriptTests", "calculatorSumPassSpec.coffee");
            var testFiles = new[] {testFile};
            var args = new Arguments
                {
                    RunServer = false,
                    VerbosityLevel = VerbosityLevel.Verbose,
                    Timeout = 10,
                    SourceFiles = sourceFiles,
                    TestFiles = testFiles
                };
            var environment = new LocalEnvironment();
            using (var webServer = new WebServer(args.RunServer, args.Directory, args.GetPort()))
            {
                var test = new Test(
                    new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(),
                                  args.SourceFiles, args.TestFiles),
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

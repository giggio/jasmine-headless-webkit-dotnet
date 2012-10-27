using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests
{
    [TestClass, Ignore]
    public class OneTestPassesWithoutServerWithJsFile
    {
        private bool runSucceeded;

        [TestInitialize]
        public void RunFiles()
        {
            var sourceFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "Scripts", "calculator.js");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "ScriptTests", "calculatorSumPassSpec.js");
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
            var test = new Test(
                new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(), args.SourceFiles, args.TestFiles),
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

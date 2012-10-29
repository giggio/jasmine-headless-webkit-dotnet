using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests
{
    [TestClass]
    public class OneTestFailsWithoutServerWithJsFile
    {
        private static bool runSucceeded;
        private static Test test;

        [ClassInitialize]
        public static void RunFiles(TestContext testContext)
        {
            var sourceFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "Scripts", "calculator.js");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "ScriptTests", "calculatorSumFailSpec.js");
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
                test = new Test(
                    new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(),
                                  args.SourceFiles, args.TestFiles),
                    webServer,
                    new Tools(environment));
                runSucceeded = test.Run();
            }
        }

        [TestMethod]
        public void VerifyFailure()
        {
            runSucceeded.Should().BeFalse();
        }

        [TestMethod]
        public void NumberOfSuccessesShouldBe0()
        {
            test.NumberOfSuccesses.Should().Be(0);
        }

        [TestMethod]
        public void NumberOfFailuresShouldBe1()
        {
            test.NumberOfFailures.Should().Be(1);
        }

    }
}

using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Js
{
    [TestClass]
    public class Cleanup
    {

        private static bool runSucceeded;
        private static Test test;
        private static string toolsDir;
        private static DirectoryInfo toolsDirInfo;

        [ClassInitialize]
        public static void RunFiles(TestContext testContext)
        {

            var sourceFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "Scripts", "calculator.coffee");
            var sourceFiles = new[] { sourceFile };
            var testFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "ScriptTests", "calculatorSumPassSpec.js");
            var testFiles = new[] { testFile };
            test = RunTestHelper.RunTestWithJSFiles(sourceFiles, testFiles);
            toolsDir = test.Tools.Environment.GetToolsDir();
            CleanupToolsDir();
            runSucceeded = test.Run();
            test.PhantomJS.Dispose();
            toolsDirInfo = new DirectoryInfo(toolsDir);
        }

        private static void CleanupToolsDir()
        {
            if (Directory.Exists(toolsDir)) Directory.Delete(toolsDir, true);
        }

        [TestMethod]
        public void AfterTestJSTempFilesAreRemoved()
        {
            var jsFiles = toolsDirInfo.GetFiles("*.js");
            jsFiles.Length.Should().Be(0);
        }

        [TestMethod]
        public void AfterTestHtmlTempFilesAreRemoved()
        {
            var htmlFiles = toolsDirInfo.GetFiles("*.html");
            htmlFiles.Length.Should().Be(0);
        }

        [TestMethod]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }


        [TestMethod]
        public void NumberOfSuccessesShouldBe1()
        {
            test.NumberOfSuccesses.Should().Be(1);
        }

    }
}
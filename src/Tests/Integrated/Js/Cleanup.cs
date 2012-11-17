using System.IO;
using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Js
{
    [TestFixture]
    public class Cleanup
    {

        private static bool runSucceeded;
        private static Test test;
        private static string toolsDir;
        private static DirectoryInfo toolsDirInfo;

        [TestFixtureSetUp]
        public static void RunFiles()
        {

            var sourceFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "Scripts", "calculator.coffee");
            var sourceFiles = new[] { sourceFile };
            var testFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "ScriptTests", "calculatorSumPassSpec.js");
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

        [Test]
        public void AfterTestJSTempFilesAreRemoved()
        {
            var jsFiles = toolsDirInfo.GetFiles("*.js");
            jsFiles.Length.Should().Be(0);
        }

        [Test]
        public void AfterTestHtmlTempFilesAreRemoved()
        {
            var htmlFiles = toolsDirInfo.GetFiles("*.html");
            htmlFiles.Length.Should().Be(0);
        }

        [Test]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }


        [Test]
        public void NumberOfSuccessesShouldBe1()
        {
            test.NumberOfSuccesses.Should().Be(1);
        }

    }
}
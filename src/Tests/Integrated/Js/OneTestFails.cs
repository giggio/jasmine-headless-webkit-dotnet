using System.IO;
using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Js
{
    [TestFixture]
    public class OneTestFails
    {
        private static bool runSucceeded;
        private static Test test;

        [TestFixtureSetUp]
        public static void RunFiles()
        {
            var sourceFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "Scripts", "calculator.js");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "ScriptTests", "calculatorSumFailSpec.js");
            var testFiles = new[] {testFile};
            test = RunTestHelper.RunTestWithJSFiles(sourceFiles, testFiles);
            runSucceeded = test.Run();
        }

        [Test]
        public void VerifyFailure()
        {
            runSucceeded.Should().BeFalse();
        }

        [Test]
        public void NumberOfSuccessesShouldBe0()
        {
            test.NumberOfSuccesses.Should().Be(0);
        }

        [Test]
        public void NumberOfFailuresShouldBe1()
        {
            test.NumberOfFailures.Should().Be(1);
        }

    }
}

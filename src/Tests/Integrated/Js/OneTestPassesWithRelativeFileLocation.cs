using System.IO;
using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Js
{
    [TestFixture]
    public class OneTestPassesWithRelativeFileLocation
    {
        private static bool runSucceeded;
        private static Test test;

        [TestFixtureSetUp]
        public static void RunFiles()
        {
            var sourceFile = Path.Combine("JasmineTests", "Scripts", "calculator.js");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine("JasmineTests", "ScriptTests", "calculatorSumPassSpec.js");
            var testFiles = new[] {testFile};
            test = RunTestHelper.RunTestWithJSFiles(sourceFiles, testFiles);
            runSucceeded = test.Run();
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

        [Test]
        public void NumberOfFailuresShouldBe0()
        {
            test.NumberOfFailures.Should().Be(0);
        }
    }
}
using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_typeScript_compiler;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Js
{
    [TestFixture]
    public class OneTypeScriptTestPasses
    {
        private static bool runSucceeded;
        private static Test test;

        private static string GetBaseDirectory()
        {
            return Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "\\..\\..";
        }

        [TestFixtureSetUp]
        public static void RunFiles()
        {
            var sourceFile = Path.Combine(GetBaseDirectory(), "TypeScriptTests", "WithHtml", "Scripts", "calculator.ts");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine(GetBaseDirectory(), "TypeScriptTests", "WithHtml", "ScriptTests", "calculatorSumPassSpec.ts");
            var testFiles = new[] {testFile};
            test = RunTestHelper.RunTestWithJSFiles(sourceFiles, testFiles);
            runSucceeded = test.Run();
        }

        [TestFixtureTearDown]
        public static void DisposePhantomJS()
        {
            test.PhantomJS.Dispose();
        }

        [Test]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }
        [Test]
        public void VerifySuccesses()
        {
            test.NumberOfSuccesses.Should().Be(1);
        }
        [Test]
        public void VerifyNoFailures()
        {
            test.NumberOfFailures.Should().Be(0);
        }
    }
}

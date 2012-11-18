using System.IO;
using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Js
{
    [TestFixture]
    public class WrongSyntaxJSFileFails
    {
        private static bool runSucceeded;
        private static Test test;

        [TestFixtureSetUp]
        public static void RunFiles()
        {
            var sourceFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "ScriptTests", "WrongSyntax.js");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "ScriptTests", "WrongSyntax.js");
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
            runSucceeded.Should().BeFalse();
        }
    }
}

using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Integrated.Js
{
    [TestFixture]
    public class WrongSyntaxJSFileFails
    {
        private static bool runSucceeded;

        [TestFixtureSetUp]
        public static void RunFiles()
        {
            var sourceFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "ScriptTests", "WrongSyntax.js");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "ScriptTests", "WrongSyntax.js");
            var testFiles = new[] {testFile};
            var test = RunTestHelper.RunTestWithJSFiles(sourceFiles, testFiles);
            runSucceeded = test.Run();
        }

        [Test]
        public void VerifyPass()
        {
            runSucceeded.Should().BeFalse();
        }
    }
}

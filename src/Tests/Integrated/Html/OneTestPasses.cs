using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Integrated.Html
{
    [TestFixture]
    public class OneTestPasses
    {
        private static bool runSucceeded;

        [TestFixtureSetUp]
        public static void RunFiles()
        {
            var test = RunTestHelper.RunTestWithHtmlFile(RunTestHelper.GetJasmineTestDirLocation(), "OneSpecPass1.1.0.html");
            runSucceeded = test.Run();
        }

        [Test]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }
    }
}

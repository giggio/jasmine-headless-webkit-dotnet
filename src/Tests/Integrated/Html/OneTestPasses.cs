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
            var test = RunTestHelper.RunTestWithHtmlFile(RunTestHelper.GetJasmineTestDirLocation("WithHtml"), "OneSpecPass1.1.0.html");
            runSucceeded = test.Run();
        }

        [Test]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }
    }
}

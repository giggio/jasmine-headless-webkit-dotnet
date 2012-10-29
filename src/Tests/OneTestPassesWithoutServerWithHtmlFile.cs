using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class OneTestPassesWithoutServerWithHtmlFile
    {
        private static bool runSucceeded;

        [ClassInitialize]
        public static void RunFiles(TestContext testContext)
        {
            var jasmineTestDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests");
            var test = RunTestHelper.RunTestWithHtmlFile(jasmineTestDir, "OneSpecPass1.1.0.html");
            runSucceeded = test.Run();
        }

        [TestMethod]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }
    }
}

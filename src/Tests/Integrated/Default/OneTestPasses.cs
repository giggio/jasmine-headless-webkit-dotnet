using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Default
{
    [TestClass]
    public class OneTestPasses
    {
        private static bool runSucceeded;
        private static Test test;

        [ClassInitialize]
        public static void RunFiles(TestContext testContext)
        {
            var jasmineTestDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests");
            test = RunTestHelper.RunTestWithDefault(jasmineTestDir);
            runSucceeded = test.Run();
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
        [TestMethod]
        public void NumberOfFailuresShouldBe0()
        {
            test.NumberOfFailures.Should().Be(0);
        }
    }
}

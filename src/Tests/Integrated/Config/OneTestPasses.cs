using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Config
{
    [TestClass]
    public class OneTestPasses
    {
        private static bool runSucceeded;
        private static Test test;

        [ClassInitialize]
        public static void RunFiles(TestContext testContext)
        {
            var configFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JasmineTests", "WithConfig", "config.js");
            test = RunTestHelper.RunTestWithConfig(configFile);
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

    }
}

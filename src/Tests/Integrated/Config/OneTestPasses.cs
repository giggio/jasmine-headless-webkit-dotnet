using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Config
{
    [TestFixture]
    public class OneTestPasses
    {
        private static bool runSucceeded;
        private static Test test;

        [TestFixtureSetUp]
        public static void RunFiles()
        {
            var configFile = Path.Combine(RunTestHelper.GetJasmineTestDirLocation(), "WithConfig", "config.js");
            test = RunTestHelper.RunTestWithConfig(configFile);
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

    }
}

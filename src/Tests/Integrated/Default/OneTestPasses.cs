using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Default
{
    [TestFixture]
    public class OneTestPasses
    {
        private static bool runSucceeded;
        private static Test test;

        [TestFixtureSetUp]
        public static void RunFiles()
        {
            test = RunTestHelper.RunTestWithDefault(RunTestHelper.GetJasmineTestDirLocation("WithHtml"));
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

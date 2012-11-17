using System;
using System.IO;
using System.Reflection;
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
            test = RunTestHelper.RunTestWithDefault(RunTestHelper.GetJasmineTestDirLocation());
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
        [Test]
        public void NumberOfFailuresShouldBe0()
        {
            test.NumberOfFailures.Should().Be(0);
        }
    }
}

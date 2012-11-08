using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestClass]
    public class ProgramTests : BaseTest
    {
        [TestMethod]
        public void SimpleRunShouldSucceed()
        {
            var jasmineRunner = automoqer.Create<JasmineRunner>();
            automoqer.GetMock<IPhantomJS>().Setup(pJS => pJS.Run()).Returns(true);
            var runSucceeded = jasmineRunner.Run();
            runSucceeded.Should().BeTrue();
        }
        [TestMethod]
        public void FailedRunShouldFail()
        {
            var jasmineRunner = automoqer.Create<JasmineRunner>();
            automoqer.GetMock<IPhantomJS>().Setup(pJS => pJS.Run()).Returns(false);
            var runSucceeded = jasmineRunner.Run();
            runSucceeded.Should().BeFalse();
        }

    }
}

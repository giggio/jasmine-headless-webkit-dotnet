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
            var program = automoqer.Create<Program>();
            automoqer.GetMock<IPhantomJS>().Setup(pJS => pJS.Run()).Returns(true);
            var runSucceeded = program.Run();
            runSucceeded.Should().BeTrue();
        }
        [TestMethod]
        public void FailedRunShouldFail()
        {
            var program = automoqer.Create<Program>();
            automoqer.GetMock<IPhantomJS>().Setup(pJS => pJS.Run()).Returns(false);
            var runSucceeded = program.Run();
            runSucceeded.Should().BeFalse();
        }

    }
}

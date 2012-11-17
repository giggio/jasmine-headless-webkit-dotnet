using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestFixture]
    public class ProgramTests : BaseTest
    {
        [Test]
        public void SimpleRunShouldSucceed()
        {
            var jasmineRunner = automoqer.Create<JasmineRunner>();
            automoqer.GetMock<IPhantomJS>().Setup(pJS => pJS.Run()).Returns(true);
            var runSucceeded = jasmineRunner.Run();
            runSucceeded.Should().BeTrue();
        }
        [Test]
        public void FailedRunShouldFail()
        {
            var jasmineRunner = automoqer.Create<JasmineRunner>();
            automoqer.GetMock<IPhantomJS>().Setup(pJS => pJS.Run()).Returns(false);
            var runSucceeded = jasmineRunner.Run();
            runSucceeded.Should().BeFalse();
        }

    }
}

using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests
{
    [TestFixture]
    public class OneTestPassesWithoutServer
    {
        private bool runSucceeded;

        [SetUp]
        public void RunFiles()
        {
            var args = new Arguments
                {
                    RunServer = false,
                    VerbosityLevel = VerbosityLevel.Verbose,
                    Timeout = 10,
                    Directory = @"c:\proj\src\jasmine-headless-webkit-dotnet\Tests\JasmineTests\",
                    FileName = "OneSpecPass1.1.0.html"
                };
            var environment = new LocalEnvironment();
            var test = new Test(
                new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(), args.Directory, args.FileName),
                new WebServer(),
                new Tools(environment),
                args);
            runSucceeded = test.Run();
        }

        [Test]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }
    }
}

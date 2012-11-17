using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestFixture]
    public class ArgumentsTests
    {

        [Test]
        public void EmptyShouldBeDefault()
        {
            var args = new Arguments();
            args.RunType.Should().Be(RunType.Default);
        }
        [Test]
        public void WithHelpTypeShouldBeHelp()
        {
            var args = new Arguments {Help = true};
            args.RunType.Should().Be(RunType.Help);
        }
        [Test]
        public void WithHelpAndOtherConsistentArgsStillTypeShouldBeHelp()
        {
            var args = new Arguments { Help = true, SourceFiles = new string[1], TestFiles = new string[1] };
            args.RunType.Should().Be(RunType.Help);
        }
        [Test]
        public void WithSourceFilesAndWithoutTestFilesTypeShouldBeHelp()
        {
            var args = new Arguments {SourceFiles = new string[1]};
            args.RunType.Should().Be(RunType.Help);
        }
        [Test]
        public void WithoutSourceFilesAndWithTestFilesTypeShouldBeHelp()
        {
            var args = new Arguments {SourceFiles = new string[1]};
            args.RunType.Should().Be(RunType.Help);
        }
        [Test]
        public void WithFileNameTypeShouldBeHtmlFile()
        {
            var args = new Arguments { FileName = "b"};
            args.RunType.Should().Be(RunType.HtmlFile);
        }
        [Test]
        public void WithSourceFilesAndTestFilesTypeShouldBeJSFiles()
        {
            var args = new Arguments { SourceFiles = new string[1], TestFiles = new string[1] };
            args.RunType.Should().Be(RunType.JSFiles);
        }
        [Test]
        public void WithConfigFileShouldBeConfigFiles()
        {
            var args = new Arguments { ConfigFile = "aa"};
            args.RunType.Should().Be(RunType.ConfigFile);
        }
    }
}

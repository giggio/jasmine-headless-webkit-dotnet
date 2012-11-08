using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestClass]
    public class ArgumentsTests
    {

        [TestMethod]
        public void EmptyShouldBeDefault()
        {
            var args = new Arguments();
            args.RunType.Should().Be(RunType.Default);
        }
        [TestMethod]
        public void WithHelpTypeShouldBeHelp()
        {
            var args = new Arguments {Help = true};
            args.RunType.Should().Be(RunType.Help);
        }
        [TestMethod]
        public void WithHelpAndOtherConsistentArgsStillTypeShouldBeHelp()
        {
            var args = new Arguments { Help = true, SourceFiles = new string[1], TestFiles = new string[1] };
            args.RunType.Should().Be(RunType.Help);
        }
        [TestMethod]
        public void WithSourceFilesAndWithoutTestFilesTypeShouldBeHelp()
        {
            var args = new Arguments {SourceFiles = new string[1]};
            args.RunType.Should().Be(RunType.Help);
        }
        [TestMethod]
        public void WithoutSourceFilesAndWithTestFilesTypeShouldBeHelp()
        {
            var args = new Arguments {SourceFiles = new string[1]};
            args.RunType.Should().Be(RunType.Help);
        }
        [TestMethod]
        public void WithFileNameTypeShouldBeHtmlFile()
        {
            var args = new Arguments { FileName = "b"};
            args.RunType.Should().Be(RunType.HtmlFile);
        }
        [TestMethod]
        public void WithSourceFilesAndTestFilesTypeShouldBeJSFiles()
        {
            var args = new Arguments { SourceFiles = new string[1], TestFiles = new string[1] };
            args.RunType.Should().Be(RunType.JSFiles);
        }
    }
}

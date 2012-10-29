using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestClass]
    public class ArgumentsTests
    {
        [TestMethod]
        public void WithSourceFilesAndTestFilesShouldBeConsistent()
        {
            var args = new Arguments {SourceFiles = new string[1], TestFiles = new string[1]};
            args.IsConsistent().Should().BeTrue();
        }
        [TestMethod]
        public void WithSourceFilesAndWithoutTestFilesShouldNotBeConsistent()
        {
            var args = new Arguments {SourceFiles = new string[1]};
            args.IsConsistent().Should().BeFalse();
        }
        [TestMethod]
        public void WithoutSourceFilesAndWithTestFilesShouldNotBeConsistent()
        {
            var args = new Arguments {SourceFiles = new string[1]};
            args.IsConsistent().Should().BeFalse();
        }
        [TestMethod]
        public void WithDirectoryAndFileNameShouldBeConsistent()
        {
            var args = new Arguments { Directory = "a", FileName = "b"};
            args.IsConsistent().Should().BeTrue();
        }
        [TestMethod]
        public void WithoutDirectoryAndFileNameShouldNotBeConsistent()
        {
            var args = new Arguments { FileName = "b"};
            args.IsConsistent().Should().BeFalse();
        }
        [TestMethod]
        public void WithDirectoryAndWithoutFileNameShouldNotBeConsistent()
        {
            var args = new Arguments { Directory = "b"};
            args.IsConsistent().Should().BeFalse();
        }
        [TestMethod]
        public void WithDirectoryAndFileNameTypeShouldBeHtmlFile()
        {
            var args = new Arguments { Directory = "a", FileName = "b"};
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

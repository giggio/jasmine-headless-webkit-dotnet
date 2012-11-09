using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestClass]
    public class GlobTests
    {
        [TestMethod]
        public void CanGetAllTextFilesInAllSubdirs()
        {
            const string pattern = "**/*.txt";
            var matches = GetMatches(pattern);
            matches.Count.Should().Be(5);
        }

        [TestMethod]
        public void CanGetAllTextFilesThatStartWithFileInAllSubdirs()
        {
            const string pattern = "**/File*.txt";
            var matches = GetMatches(pattern);
            matches.Count.Should().Be(4);
        }

        [TestMethod]
        public void CanGetAllTextFilesThatStartWithFileInSubDirsThatStartWithDir()
        {
            const string pattern = "**/Dir?/File*.txt";
            var matches = GetMatches(pattern);
            matches.Count.Should().Be(4);
        }
        [TestMethod]
        public void CanGetZeroTextFilesThatStartWithFileInSubDirsThatStartWithNonExistentName()
        {
            const string pattern = "**/WrongDir*/File*.txt";
            var matches = GetMatches(pattern);
            matches.Count.Should().Be(0);
        }

        [TestMethod]
        public void CanGetAllTextFilesNamedOtherFileInSubdirDirA()
        {
            const string pattern = "**/dirA/OtherFile?.*";
            var matches = GetMatches(pattern);
            matches.Count.Should().Be(2);
        }

        [TestMethod]
        public void CanGetAllTextFilesNamedOtherFileLogInSubdirDirA()
        {
            const string pattern = "**/dirA/OtherFile1.log";
            var matches = GetMatches(pattern);
            matches.Count.Should().Be(1);
            matches.First().Should().BeEquivalentTo(GetGlobTestDir().Replace("\\", "/") + "/dirA/OtherFile1.log");
        }

        private static List<string> GetMatches(string pattern)
        {
            var globTestDir = GetGlobTestDir();
            globTestDir = globTestDir.Replace("\\", "/") + "/";
            var matches = Glob.GetMatches(string.Format("{0}" + pattern, globTestDir)).ToList();
            return matches;
        }

        private static string GetGlobTestDir()
        {
            var globTestDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "GlobTestFiles");
            return globTestDir;
        }
    }
}

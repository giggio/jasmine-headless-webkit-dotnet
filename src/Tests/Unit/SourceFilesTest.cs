﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestFixture]
    public class SourceFilesTest
    {
        [Test]
        public void WhenCompiledFixesFileNamesAndPathsCorrectly()
        {
            var files = new[] {@"d:\a\f1.js", @"d:\a\f2.coffee"};
            var sourceFiles = new SourceFilesStub(files);
            var jsFiles = sourceFiles.Compile(@"c:\temp");
            Regex.Match(jsFiles[0], @"c:\\temp\\f1-\d+.js").Value.Should().Be(jsFiles[0]);
            Regex.Match(jsFiles[1], @"c:\\temp\\f2-\d+.js").Value.Should().Be(jsFiles[1]);
        }

        private class SourceFilesStub : SourceFiles
        {
            public SourceFilesStub(IEnumerable<string> files) : base(files) {}

            protected override List<SourceFile> CreateSourceFiles(IEnumerable<string> files)
            {
                return new List<SourceFile>(files.Select(f => new SourceFileStub(f)));
            }
        }

        private class SourceFileStub : SourceFile
        {
            public SourceFileStub(string path) : base(path) {}
            public override void Compile(string directory)
            {
                CreateCompiledPath(directory);
            }
        }
    }

    
}

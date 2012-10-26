using System.IO;

namespace jasmine_headless_webkit_dotnet
{
    /// <summary>
    /// Runinng like this: phantomjs.exe run_jasmine_test.coffee c:\testes\testes.html
    /// </summary>
    public class RunPhantomJSFromHtmlFileLocally : PhantomJSRunStragegy
    {
        private readonly string directory;
        private readonly string fileName;

        public RunPhantomJSFromHtmlFileLocally(string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut, string directory, string fileName)
            : base(phantomFileLocation, jasmineTestFileLocation, verbosityLevel, timeOut)
        {
            this.directory = directory;
            this.fileName = fileName;
        }

        public override string BuildArgs()
        {
            var fullFileName = Path.Combine(directory, fileName).Replace('\\', '/');
            var phantomArgs = string.Format("{0} file:///{1}", jasmineTestFileLocation, fullFileName);
            return phantomArgs;
        }
    }
}
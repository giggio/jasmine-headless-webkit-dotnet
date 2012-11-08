using System.IO;

namespace jasmine_headless_webkit_dotnet
{
    /// <summary>
    /// Runinng like this: phantomjs.exe run_jasmine_test.coffee c:\testes\testes.html
    /// </summary>
    public class RunPhantomJSFromHtmlFileLocally : PhantomJS
    {
        private readonly string fileName;

        public RunPhantomJSFromHtmlFileLocally(string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut, string fileName)
            : base(phantomFileLocation, jasmineTestFileLocation, verbosityLevel, timeOut)
        {
            this.fileName = fileName;
        }

        protected override string BuildArgs()
        {
            var phantomArgs = string.Format("{0} file:///{1}", jasmineTestFileLocation, fileName.Replace('\\', '/'));
            return phantomArgs;
        }
    }
}
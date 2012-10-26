namespace jasmine_headless_webkit_dotnet
{
    /// <summary>
    /// Running like this: phantomjs.exe run_jasmine_test.coffee http://localhost:14815/testes.html
    /// </summary>
    public class RunPhantomJSFromHtmlFileInServer : PhantomJSRunStragegy
    {
        private readonly int port;
        private readonly string fileName;

        public RunPhantomJSFromHtmlFileInServer(string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut, int port, string fileName)
            : base(phantomFileLocation, jasmineTestFileLocation, verbosityLevel, timeOut)
        {
            this.port = port;
            this.fileName = fileName;
        }

        public override string BuildArgs()
        {
            var phantomArgs = string.Format("{0} http://localhost:{1}/{2}", jasmineTestFileLocation, port, fileName);
            return phantomArgs;
        }
    }
}
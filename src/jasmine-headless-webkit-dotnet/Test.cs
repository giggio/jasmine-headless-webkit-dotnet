namespace jasmine_headless_webkit_dotnet
{
    public class Test
    {
        private readonly IPhantomJS phantomJS;
        private readonly IWebServer webServer;
        private readonly ITools tools;

        public Test(IPhantomJS phantomJS, ITools tools, IWebServer webServer = null)
        {
            this.phantomJS = phantomJS;
            this.webServer = webServer;
            this.tools = tools;
        }

        public int NumberOfSuccesses
        {
            get { return phantomJS.NumberOfSuccesses; }
        }

        public int NumberOfFailures
        {
            get { return phantomJS.NumberOfFailures; }
        }

        public bool Run()
        {
            tools.Hidrate();
            if (webServer != null) webServer.Run();
            var testResult = phantomJS.Run();
            return testResult;
        }
    }
}
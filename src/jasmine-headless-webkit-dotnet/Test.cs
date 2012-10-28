namespace jasmine_headless_webkit_dotnet
{
    public class Test
    {
        private readonly IPhantomJS phantomJS;
        private readonly IWebServer webServer;
        private readonly ITools tools;

        public Test(IPhantomJS phantomJS, IWebServer webServer, ITools tools)
        {
            this.phantomJS = phantomJS;
            this.webServer = webServer;
            this.tools = tools;
        }

        public int NumberOfSuccesses
        {
            get { return phantomJS.NumberOfSuccesses; }
        }

        public bool Run()
        {
            tools.Hidrate();
            webServer.RunServer();
            var testResult = phantomJS.Run();
            return testResult;
        }
    }
}
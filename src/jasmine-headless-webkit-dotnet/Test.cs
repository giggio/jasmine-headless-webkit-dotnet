namespace jasmine_headless_webkit_dotnet
{
    public class Test
    {
        private readonly IPhantomJS phantomJS;
        private readonly IWebServer webServer;
        private readonly ITools tools;
        private readonly Arguments args;

        public Test(IPhantomJS phantomJS, IWebServer webServer, ITools tools, Arguments args)
        {
            this.phantomJS = phantomJS;
            this.webServer = webServer;
            this.tools = tools;
            this.args = args;
        }

        public bool Run()
        {
            tools.Hidrate();
            if (args.RunServer)
                webServer.RunServer(args.Directory, args.GetPort());
            var testResult = phantomJS.Run();
            webServer.Dispose();
            return testResult;
        }
    }
}

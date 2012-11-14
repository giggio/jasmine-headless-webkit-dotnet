namespace jasmine_headless_webkit_dotnet
{
    public class Test
    {
        private readonly IPhantomJS phantomJS;
        public IPhantomJS PhantomJS { get { return phantomJS; } }
        private readonly ITools tools;
        public ITools Tools { get { return tools; } }

        public Test(IPhantomJS phantomJS, ITools tools)
        {
            this.phantomJS = phantomJS;
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
            var testResult = phantomJS.Run();
            return testResult;
        }
    }
}
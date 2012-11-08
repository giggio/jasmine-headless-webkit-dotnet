namespace jasmine_headless_webkit_dotnet
{
    public class JasmineRunner
    {
        private readonly ITools tools;
        private readonly IPhantomJS phantomJS;

        public JasmineRunner(ITools tools, IPhantomJS phantomJS)
        {
            this.tools = tools;
            this.phantomJS = phantomJS;
        }

        public bool Run()
        {
            var test = new Test(phantomJS, tools);
            var runSucceeded = test.Run();
            return runSucceeded;
        }
    }
}

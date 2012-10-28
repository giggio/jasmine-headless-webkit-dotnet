namespace jasmine_headless_webkit_dotnet
{
    public class PhantomJS : IPhantomJS
    {
        private readonly string phantomFileLocation;
        private readonly VerbosityLevel verbosityLevel;
        private readonly string jasmineTestFileLocation;
        private readonly PhantomJSRunStragegy runStragegy;

        public PhantomJS(ILocalEnvironment environment, VerbosityLevel verbosityLevel)
        {
            this.verbosityLevel = verbosityLevel;
            jasmineTestFileLocation = environment.GetRunJasmineTestFileLocation();
            phantomFileLocation = environment.GetPhantomJSExeFileLocation();            
        }
        public PhantomJS(ILocalEnvironment environment, bool runningInServer, VerbosityLevel verbosityLevel, int timeOut, int port, string directory, string fileName) : 
            this(environment,verbosityLevel)
        {
            if (runningInServer)
            {
                runStragegy = new RunPhantomJSFromHtmlFileInServer(phantomFileLocation, jasmineTestFileLocation, verbosityLevel,
                                                           timeOut, port, fileName);
            }
            else
            {
                runStragegy = new RunPhantomJSFromHtmlFileLocally(phantomFileLocation, jasmineTestFileLocation, verbosityLevel,
                                                           timeOut, directory, fileName);
            }
        }
        public PhantomJS(ILocalEnvironment environment, bool runningInServer, VerbosityLevel verbosityLevel, int timeOut, int port, string[] sourceFiles, string[] testFiles)
            : this(environment, verbosityLevel)
        {
            runStragegy = new RunPhantomJSFromJSFilesLocally(environment, phantomFileLocation, jasmineTestFileLocation, verbosityLevel,
                                                           timeOut, sourceFiles, testFiles);
        }

        public bool Run()
        {
            return runStragegy.Run();
        }

        public int NumberOfSuccesses { get { return runStragegy.NumberOfSuccesses; } }
    }

    public interface IPhantomJS
    {
        bool Run();
        int NumberOfSuccesses { get; }
    }
}
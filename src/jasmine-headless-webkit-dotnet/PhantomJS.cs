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
        public PhantomJS(ILocalEnvironment environment, VerbosityLevel verbosityLevel, int timeOut, string fileName) : 
            this(environment,verbosityLevel)
        {
                runStragegy = new RunPhantomJSFromHtmlFileLocally(phantomFileLocation, jasmineTestFileLocation, verbosityLevel,
                                                           timeOut, fileName);
        }
        public PhantomJS(ILocalEnvironment environment, VerbosityLevel verbosityLevel, int timeOut, string[] sourceFiles, string[] testFiles)
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
        public int NumberOfFailures { get { return runStragegy.NumberOfFailures; } }
    }

    public interface IPhantomJS
    {
        bool Run();
        int NumberOfSuccesses { get; }
        int NumberOfFailures { get; }
    }
}
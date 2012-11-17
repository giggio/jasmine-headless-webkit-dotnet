using System;

namespace jasmine_headless_webkit_dotnet
{
    class PhantomJSFactory
    {
        private readonly Arguments args;
        private readonly ILocalEnvironment environment;

        public PhantomJSFactory(Arguments args, ILocalEnvironment environment)
        {
            this.args = args;
            this.environment = environment;
        }

        public IPhantomJS Create()
        {
            IPhantomJS phantomJS;
            switch (args.RunType)
            {
                case RunType.HtmlFile:
                    phantomJS = new PhantomJSFromHtmlFile(environment.GetPhantomJSExeFileLocation(), environment.GetRunJasmineTestFileLocation(), args.VerbosityLevel, args.GetTimeOut(), args.FileName);
                    break;
                case RunType.JSFiles:
                    phantomJS = new PhantomJSFromJSFiles(environment, environment.GetPhantomJSExeFileLocation(), environment.GetRunJasmineTestFileLocation(), args.VerbosityLevel, args.GetTimeOut(), args.SourceFiles, args.TestFiles);
                    break;
                case RunType.ConfigFile:
                    phantomJS = new PhantomJSFromConfigFile(environment, environment.GetPhantomJSExeFileLocation(), environment.GetRunJasmineTestFileLocation(), args.VerbosityLevel, args.GetTimeOut(), args.ConfigFile);
                    break;
                case RunType.Default:
                    phantomJS = new PhantomJSDefault(environment, environment.GetPhantomJSExeFileLocation(), environment.GetRunJasmineTestFileLocation(), args.VerbosityLevel, args.GetTimeOut());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return phantomJS;
        }
    }
}

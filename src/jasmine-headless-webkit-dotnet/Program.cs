using System;
using Args;

namespace jasmine_headless_webkit_dotnet
{
    public class Program
    {
        private readonly ILocalEnvironment environment;
        private readonly IWebServer webServer;
        private readonly ITools tools;
        private readonly IPhantomJS phantomJS;

        static int Main(string[] argumentsArray)
        {
            var args = Args.Configuration.Configure<Arguments>().AsFluent()
                .ParsesArgumentsWith(typeof(string[]), new ArrayOfStringConverter())
                .Initialize()
                .CreateAndBind(argumentsArray);
            if (!args.IsConsistent())
            {
                args.WriteHelp();
                return 1;
            }
            var environment = new LocalEnvironment();
            var phantomJS = GetPhantomJS(args, environment);
            using (var webServer = new WebServer(args.RunServer, args.Directory, args.GetPort()))
            {
                var program = new Program(environment,
                    webServer,
                    new Tools(environment),
                    phantomJS);
                var runSucceeded = program.Run();
                return runSucceeded ? 0 : 1;
            }
        }

        private static IPhantomJS GetPhantomJS(Arguments args, LocalEnvironment environment)
        {
            IPhantomJS phantomJS;
            switch (args.RunType)
            {
                case RunType.HtmlFile:
                    phantomJS = new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(),
                                              args.GetPort(), args.Directory, args.FileName);
                    break;
                case RunType.JSFiles:
                    phantomJS = new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(),
                                              args.GetPort(), args.SourceFiles, args.TestFiles);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return phantomJS;
        }

        public Program(ILocalEnvironment environment, IWebServer webServer, ITools tools, IPhantomJS phantomJS)
        {
            this.environment = environment;
            this.webServer = webServer;
            this.tools = tools;
            this.phantomJS = phantomJS;
        }

        public bool Run()
        {
            var test = new Test(
                phantomJS,
                webServer,
                tools);
            var runSucceeded = test.Run();
            return runSucceeded;
        }
    }
}
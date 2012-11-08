using System;
using Args;

namespace jasmine_headless_webkit_dotnet
{
    public class Program
    {
        private readonly ITools tools;
        private readonly IPhantomJS phantomJS;

        static int Main(string[] argumentsArray)
        {
            var args = Args.Configuration.Configure<Arguments>().AsFluent()
                .ParsesArgumentsWith(typeof(string[]), new ArrayOfStringConverter())
                .Initialize()
                .CreateAndBind(argumentsArray);
            if (args.RunType == RunType.Help)
            {
                args.WriteHelp();
                return 1;
            }
            var environment = new LocalEnvironment();
            var phantomJS = new PhantomJSFactory(args, environment).Create();
            var program = new Program(
                new Tools(environment),
                phantomJS);
            var runSucceeded = program.Run();
            return runSucceeded ? 0 : 1;
        }

        public Program(ITools tools, IPhantomJS phantomJS)
        {
            this.tools = tools;
            this.phantomJS = phantomJS;
        }

        public bool Run()
        {
            var test = new Test(
                phantomJS,
                tools);
            var runSucceeded = test.Run();
            return runSucceeded;
        }
    }
}
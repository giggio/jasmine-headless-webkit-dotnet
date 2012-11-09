using Args;

namespace jasmine_headless_webkit_dotnet
{
    public class Program
    {
        static int Main(string[] argumentsArray)
        {
            var args = GetArguments(argumentsArray);
            if (args.RunType == RunType.Help)
            {
                args.WriteHelp();
                return 1;
            }
            var environment = new LocalEnvironment();
            var phantomJS = new PhantomJSFactory(args, environment).Create();
            var program = new JasmineRunner(new Tools(environment), phantomJS);
            var runSucceeded = program.Run();
            return runSucceeded ? 0 : 1;
        }

        private static Arguments GetArguments(string[] argumentsArray)
        {
            var args = Args.Configuration.Configure<Arguments>().AsFluent()
                .ParsesArgumentsWith(typeof (string[]), new ArrayOfStringConverter())
                .Initialize()
                .CreateAndBind(argumentsArray);
            return args;
        }
    }
}
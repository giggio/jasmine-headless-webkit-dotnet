using Args;

namespace jasmine_headless_webkit_dotnet
{
    public class Program
    {
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
            var test = new Test(
                new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(), args.Directory, args.FileName), 
                new WebServer(), 
                new Tools(environment), 
                args);
            var runSucceeded = test.Run();
            return runSucceeded ? 0 : 1;
        }
    }
}

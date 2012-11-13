using System;
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
            IPhantomJS phantomJS;
            try
            {
                phantomJS = new PhantomJSFactory(args, environment).Create();
            }
            catch (JasmineConfigurationFileDoesNotExistException)
            {
                WriteError("Jasmine configuration file for default run could not be found at '{0}'.", environment.GetJasmineConfigurationFileLocation());
                args.WriteHelp();
                return 1;
            }
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

        private static void WriteError(string text, params object[] args)
        {
            WriteLine(ConsoleColor.Red, text, args);    
        }

        private static void WriteLine(ConsoleColor color, string text, params object[] args)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (args.Length == 0)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine(text, args);
            }
            Console.ForegroundColor = originalColor;
        }
    }
}
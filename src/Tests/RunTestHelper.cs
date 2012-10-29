using jasmine_headless_webkit_dotnet;

namespace Tests
{
    public class RunTestHelper
    {
        public static Test RunTestWithHtmlFile(string jasmineTestDir, string fileName)
        {
            var args = new Arguments
            {
                RunServer = false,
                VerbosityLevel = VerbosityLevel.Verbose,
                Timeout = 10,
                Directory = jasmineTestDir,
                FileName = fileName
            };
            var environment = new LocalEnvironment();
            var test = new Test(
                new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(),
                                args.Directory, args.FileName),
                new Tools(environment));
            return test;
        }
        public static Test RunTestWithJSFiles(string[] sourceFiles, string[] testFiles)
        {
            var args = new Arguments
            {
                RunServer = false,
                VerbosityLevel = VerbosityLevel.Verbose,
                Timeout = 10,
                SourceFiles = sourceFiles,
                TestFiles = testFiles
            };
            var environment = new LocalEnvironment();
            var test = new Test(
                new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(), args.SourceFiles, args.TestFiles),
                new Tools(environment));
            return test;
        }
        public static Test RunTestWithJSFilesAndServer(string[] sourceFiles, string[] testFiles)
        {
            var args = new Arguments
            {
                RunServer = false,
                VerbosityLevel = VerbosityLevel.Verbose,
                Timeout = 10,
                SourceFiles = sourceFiles,
                TestFiles = testFiles
            };
            var environment = new LocalEnvironment();
            //todo: stop server
            var webServer = new WebServer(args.RunServer, args.Directory, args.GetPort());
            var test = new Test(
                new PhantomJS(environment, args.RunServer, args.VerbosityLevel, args.GetTimeOut(), args.GetPort(),
                                args.SourceFiles, args.TestFiles),
                new Tools(environment),
                webServer);
            return test;
        }
    }
}

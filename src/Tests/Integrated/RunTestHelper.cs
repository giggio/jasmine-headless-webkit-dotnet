using System.IO;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated
{
    public class RunTestHelper
    {
        public static Test RunTestWithHtmlFile(string jasmineTestDir, string fileName)
        {
            var args = new Arguments
            {
                VerbosityLevel = VerbosityLevel.Verbose,
                Timeout = 10,
                FileName = Path.Combine(jasmineTestDir, fileName)
            };
            var environment = new LocalEnvironment();
            var test = new Test(
                new PhantomJSFromHtmlFile(environment.GetPhantomJSExeFileLocation(), environment.GetRunJasmineTestFileLocation(), args.VerbosityLevel, args.GetTimeOut(), args.FileName),
                new Tools(environment));
            return test;
        }
        public static Test RunTestWithJSFiles(string[] sourceFiles, string[] testFiles)
        {
            var args = new Arguments
            {
                VerbosityLevel = VerbosityLevel.Verbose,
                Timeout = 10,
                SourceFiles = sourceFiles,
                TestFiles = testFiles
            };
            var environment = new LocalEnvironment();
            var test = new Test(
                new PhantomJSFromJSFiles(environment, environment.GetPhantomJSExeFileLocation(), environment.GetRunJasmineTestFileLocation(), args.VerbosityLevel, args.GetTimeOut(), args.SourceFiles, args.TestFiles),
                new Tools(environment));
            return test;
        }
    }
}

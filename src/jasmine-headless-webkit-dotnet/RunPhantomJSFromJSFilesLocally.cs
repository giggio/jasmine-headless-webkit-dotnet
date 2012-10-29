using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using jasmine_headless_webkit_dotnet.Properties;

namespace jasmine_headless_webkit_dotnet
{
    public class RunPhantomJSFromJSFilesLocally : PhantomJSRunStragegy
    {
        private readonly ILocalEnvironment environment;
        private readonly string[] sourceFiles;
        private readonly string[] testFiles;
        private string fullHtmlTestFileLocation;

        public RunPhantomJSFromJSFilesLocally(ILocalEnvironment environment, string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut, string[] sourceFiles, string[] testFiles)
            : base(phantomFileLocation, jasmineTestFileLocation, verbosityLevel, timeOut)
        {
            this.environment = environment;
            this.sourceFiles = sourceFiles;
            this.testFiles = testFiles;
        }

        public override string BuildArgs()
        {
            CreateHtmlTestFile();
            var fullHtmlTestFileLocationCorrected = fullHtmlTestFileLocation.Replace('\\', '/');
            var phantomArgs = string.Format("{0} file:///{1}", jasmineTestFileLocation, fullHtmlTestFileLocationCorrected);
            return phantomArgs;
        }

        private void CreateHtmlTestFile()
        {
            var htmlTestFileLocation = string.Format("jasmine-headless-webkit-dotnet-{0}.html",
                                                     new Random().Next(0, 1000000));
            fullHtmlTestFileLocation = Path.Combine(environment.GetToolsDir(), htmlTestFileLocation);
            var sourceFilesWithRelativeNames = FixRelativeLocation(sourceFiles);
            var sourceFilesFormated = sourceFilesWithRelativeNames.Aggregate(string.Empty, (current, sourceFile) => current + string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>\n", sourceFile));
            var testFilesWithFelativeNames = FixRelativeLocation(testFiles);
            var testFilesFormated = testFilesWithFelativeNames.Aggregate(string.Empty, (current, sourceFile) => current + string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>\n", sourceFile));
            var htmlTestFileContent = Resources._110_test_html.Replace("<!--source files-->", sourceFilesFormated).Replace("<!--test files-->", testFilesFormated);
            File.WriteAllText(fullHtmlTestFileLocation, htmlTestFileContent);
        }

        private string[] FixRelativeLocation(string[] files)
        {
            var fullFileNames = new List<string>();
            foreach (var file in files)
            {
                var pathIsRelative = file.Length > 0 && Path.GetPathRoot(file).Length == 0;
                if (pathIsRelative)
                {
                    fullFileNames.Add(Path.Combine(environment.GetRunDir(), file));
                }
                else
                {
                    fullFileNames.Add(file);
                }
            }
            return fullFileNames.ToArray();
        }
    }
}
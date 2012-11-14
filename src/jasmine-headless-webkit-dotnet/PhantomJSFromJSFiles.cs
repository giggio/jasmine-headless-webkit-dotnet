using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using jasmine_headless_webkit_dotnet.Properties;

namespace jasmine_headless_webkit_dotnet
{
    public class PhantomJSFromJSFiles : PhantomJS
    {
        protected readonly ILocalEnvironment environment;
        protected string[] sourceFiles;
        protected string[] testFiles;
        private string fullHtmlTestFileLocation;

        public PhantomJSFromJSFiles(ILocalEnvironment environment, string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut, string[] sourceFiles, string[] testFiles)
            : base(phantomFileLocation, jasmineTestFileLocation, verbosityLevel, timeOut)
        {
            this.environment = environment;
            this.sourceFiles = sourceFiles;
            this.testFiles = testFiles;
        }

        protected override string BuildArgs()
        {
            FixRelativeLocations();
            CreateJSFromCoffeeFiles();
            CreateHtmlTestFile();
            var fullHtmlTestFileLocationCorrected = fullHtmlTestFileLocation.Replace('\\', '/');
            var phantomArgs = string.Format("{0} file:///{1}", jasmineTestFileLocation, fullHtmlTestFileLocationCorrected);
            return phantomArgs;
        }

        private void FixRelativeLocations()
        {
            sourceFiles = FixRelativeLocation(sourceFiles);
            testFiles = FixRelativeLocation(testFiles);
        }
        
        private void CreateJSFromCoffeeFiles()
        {
            sourceFiles = new SourceFiles(sourceFiles).Compile(environment.GetToolsDir());
            testFiles = new SourceFiles(testFiles).Compile(environment.GetToolsDir());            
        }

        private void CreateHtmlTestFile()
        {
            var htmlTestFileLocation = string.Format("jasmine-headless-webkit-dotnet-{0}.html", new Random().Next(0, 1000000));
            fullHtmlTestFileLocation = Path.Combine(environment.GetToolsDir(), htmlTestFileLocation);
            var sourceFilesWithScriptTag = sourceFiles.Aggregate(string.Empty, (current, sourceFile) => current + string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>\n", sourceFile));
            var testFilesWithScriptTag = testFiles.Aggregate(string.Empty, (current, sourceFile) => current + string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>\n", sourceFile));
            var htmlTestFileContent = Resources._110_test_html
                .Replace("<!--source files-->", sourceFilesWithScriptTag)
                .Replace("<!--test files-->", testFilesWithScriptTag);
            File.WriteAllText(fullHtmlTestFileLocation, htmlTestFileContent);
        }

        private string[] FixRelativeLocation(IEnumerable<string> files)
        {
            var fullFileNames = new List<string>();
            foreach (var file in files)
            {
                var pathIsRelative = file.Length > 0 && Path.GetPathRoot(file).Length == 0;
                fullFileNames.Add(pathIsRelative ? Path.Combine(environment.GetRunDir(), file) : file);
            }
            return fullFileNames.ToArray();
        }

        public override void Dispose()
        {
            base.Dispose();
            DeleteTempFiles();
        }

        private void DeleteTempFiles()
        {
            DeleteTempHtmlFile();
            DeleteTempJsFiles();
        }

        private void DeleteTempJsFiles()
        {
            var files = from f in sourceFiles.Union(testFiles)
                        where string.Compare(Path.GetDirectoryName(f), environment.GetToolsDir(), ignoreCase:true) == 0
                        select f;
            foreach (var file in files)
            {
                if (!File.Exists(file)) continue;
                try
                {
                    File.Delete(file);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("Error deleting temp js file: \n{0}", exception);
                    if (verbosityLevel > VerbosityLevel.Normal)
                    {
                        Console.WriteLine("Error deleting temp js file: \n{0}", exception);
                    }
                }
            }
        }

        private void DeleteTempHtmlFile()
        {
            if (File.Exists(fullHtmlTestFileLocation))
            {
                try
                {
                    File.Delete(fullHtmlTestFileLocation);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("Error deleting temp html file: \n{0}", exception);
                    if (verbosityLevel > VerbosityLevel.Normal)
                    {
                        Console.WriteLine("Error deleting temp html file: \n{0}", exception);
                    }
                }
            }
        }
    }
}
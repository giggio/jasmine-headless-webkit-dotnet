using System;
using System.IO;

namespace jasmine_headless_webkit_dotnet
{
    public class LocalEnvironment : ILocalEnvironment
    {
        public string GetPhantomFileLocation()
        {
            return Path.Combine(GetToolsDir(), "phantomjs.exe");
        }

        public string GetRunJasmineTestFileLocation()
        {
            return Path.Combine(GetToolsDir(), "run_jasmine_test.coffee");
        }
        public string GetToolsDir()
        {
            return Path.Combine(Environment.CurrentDirectory, ".jasmine-headless-webkit-dotnet");
        }
    }

    public interface ILocalEnvironment
    {
        string GetPhantomFileLocation();
        string GetRunJasmineTestFileLocation();
        string GetToolsDir();
    }
}
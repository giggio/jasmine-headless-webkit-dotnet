using System;
using System.IO;

namespace jasmine_headless_webkit_dotnet
{
    public class LocalEnvironment : ILocalEnvironment
    {
        public string GetPhantomJSExeFileLocation()
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

        public string GetJasmineConsoleRunnerJSFileLocation(int jasmineVersion)
        {
            return Path.Combine(GetJasmineDir(jasmineVersion), "console-runner.js");
        }

        public string GetJasmineJasmineHtmlJSFileLocation(int jasmineVersion)
        {
            return Path.Combine(GetJasmineDir(jasmineVersion), "jasmine-html.js");
        }

        public string GetJasmineJasmineCSSFileLocation(int jasmineVersion)
        {
            return Path.Combine(GetJasmineDir(jasmineVersion), "jasmine.css");
        }

        public string GetJasmineJasmineJSFileLocation(int jasmineVersion)
        {
            return Path.Combine(GetJasmineDir(jasmineVersion), "jasmine.js");
        }

        public string GetJasmineDir(int jasmineVersion)
        {
            var major = Math.Abs(jasmineVersion/100);
            var minor = Math.Abs((jasmineVersion - (major*100))/10);
            var revision = Math.Abs((jasmineVersion - (major*100) - (minor*10)));
            var jasmineDir = string.Format("jasmine-{0}.{1}.{2}", major, minor, revision);
            return Path.Combine(GetToolsDir(), jasmineDir);
        }
    }

    public interface ILocalEnvironment
    {
        string GetPhantomJSExeFileLocation();
        string GetRunJasmineTestFileLocation();
        string GetToolsDir();
        string GetJasmineConsoleRunnerJSFileLocation(int jasmineVersion);
        string GetJasmineJasmineHtmlJSFileLocation(int jasmineVersion);
        string GetJasmineJasmineCSSFileLocation(int jasmineVersion);
        string GetJasmineJasmineJSFileLocation(int jasmineVersion);
        string GetJasmineDir(int jasmineVersion);
    }
}
using System.IO;

namespace jasmine_headless_webkit_dotnet
{
    public class Tools : ITools
    {
        private readonly ILocalEnvironment environment;
        public ILocalEnvironment Environment { get { return environment; } }

        public Tools(ILocalEnvironment environment)
        {
            this.environment = environment;
        }

        public void Hidrate()
        {
            var toolsDirectoryInfo = new DirectoryInfo(environment.GetToolsDir());
            if (!toolsDirectoryInfo.Exists) toolsDirectoryInfo.Create();

            var jasmineDirectoryInfo = new DirectoryInfo(environment.GetJasmineDir(110));
            if (!jasmineDirectoryInfo.Exists) jasmineDirectoryInfo.Create();

            WriteIfDoesNotExist(environment.GetPhantomJSExeFileLocation(), Properties.Resources.phantomjs_exe);
            WriteIfDoesNotExist(environment.GetRunJasmineTestFileLocation(), Properties.Resources.run_jasmine_test_coffee);
            WriteIfDoesNotExist(environment.GetJasmineConsoleRunnerJSFileLocation(110), Properties.Resources._110_console_runner_js);
            WriteIfDoesNotExist(environment.GetJasmineJasmineHtmlJSFileLocation(110), Properties.Resources._110_jasmine_html_js);
            WriteIfDoesNotExist(environment.GetJasmineJasmineCSSFileLocation(110), Properties.Resources._110_jasmine_css);
            WriteIfDoesNotExist(environment.GetJasmineJasmineJSFileLocation(110), Properties.Resources._110_jasmine_js);
        }

        private void WriteIfDoesNotExist(string fileLocation, string fileContent)
        {
            if (File.Exists(fileLocation)) return;
            File.WriteAllText(fileLocation, fileContent);
        }

        private void WriteIfDoesNotExist(string fileLocation, byte[] fileBytes)
        {
            if (File.Exists(fileLocation)) return;
            File.WriteAllBytes(fileLocation, fileBytes);
        }
    }

    public interface ITools
    {
        void Hidrate();
        ILocalEnvironment Environment { get; }
    }
}
using System.IO;

namespace jasmine_headless_webkit_dotnet
{
    public class Tools : ITools
    {
        private readonly ILocalEnvironment environment;

        public Tools(ILocalEnvironment environment)
        {
            this.environment = environment;
        }

        public void Hidrate()
        {
            var toolsDirectoryInfo = new DirectoryInfo(environment.GetToolsDir());
            if (!toolsDirectoryInfo.Exists) toolsDirectoryInfo.Create();
            toolsDirectoryInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

            var jasmineDirectoryInfo = new DirectoryInfo(environment.GetJasmineDir(110));
            if (!jasmineDirectoryInfo.Exists) jasmineDirectoryInfo.Create();

            File.WriteAllBytes(environment.GetPhantomJSExeFileLocation(), Properties.Resources.phantomjs_exe);
            File.WriteAllBytes(environment.GetRunJasmineTestFileLocation(), Properties.Resources.run_jasmine_test_coffee);
            File.WriteAllText(environment.GetJasmineConsoleRunnerJSFileLocation(110), Properties.Resources._110_console_runner_js);
            File.WriteAllText(environment.GetJasmineJasmineHtmlJSFileLocation(110), Properties.Resources._110_jasmine_html_js);
            File.WriteAllText(environment.GetJasmineJasmineCSSFileLocation(110), Properties.Resources._110_jasmine_css);
            File.WriteAllText(environment.GetJasmineJasmineJSFileLocation(110), Properties.Resources._110_jasmine_js);
        }

    }

    public interface ITools
    {
        void Hidrate();
    }
}
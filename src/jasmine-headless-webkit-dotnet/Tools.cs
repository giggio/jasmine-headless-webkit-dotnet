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

            File.WriteAllBytes(environment.GetPhantomFileLocation(), Properties.Resources.phantomjs_exe);
            File.WriteAllBytes(environment.GetRunJasmineTestFileLocation(), Properties.Resources.run_jasmine_test_coffee);
        }

    }

    public interface ITools
    {
        void Hidrate();
    }
}
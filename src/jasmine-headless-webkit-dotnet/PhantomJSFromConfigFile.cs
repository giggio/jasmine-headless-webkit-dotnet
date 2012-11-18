using System.IO;
using GlobDir;
using Newtonsoft.Json;
using System.Linq;

namespace jasmine_headless_webkit_dotnet
{
    public class PhantomJSFromConfigFile : PhantomJSFromJSFiles
    {
        private readonly string configFile;

        public PhantomJSFromConfigFile(ILocalEnvironment environment, string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut, string configFile)
            : base(environment, phantomFileLocation, jasmineTestFileLocation, verbosityLevel, timeOut, null, null)
        {
            this.configFile = environment.GetJasmineConfigurationFileLocation(configFile); ;
            if (!File.Exists(this.configFile))
            {
                throw new JasmineConfigurationFileDoesNotExistException();
            }
        }

        protected override string BuildArgs()
        {
            var runDir = Path.GetDirectoryName(configFile);
            var configText = File.ReadAllText(configFile);
            var config = JsonConvert.DeserializeObject<JasmineConfig>(configText);
            sourceFiles = config.src_files.SelectMany(f => Glob.GetMatches(CanonicalizePath(Path.Combine(runDir, config.src_dir, f)))).ToArray();
            testFiles = config.spec_files.SelectMany(f => Glob.GetMatches(CanonicalizePath(Path.Combine(runDir, config.spec_dir, f)))).ToArray();
            return base.BuildArgs();
        }

        private static string CanonicalizePath(string path)
        {
            return path.Replace('\\', '/');
        }
    }
}
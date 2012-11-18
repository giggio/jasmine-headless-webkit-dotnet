using System.IO;
using GlobDir;
using Newtonsoft.Json;
using System.Linq;

namespace jasmine_headless_webkit_dotnet
{
    public class PhantomJSDefault : PhantomJSFromJSFiles
    {
        public PhantomJSDefault(ILocalEnvironment environment, string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut)
            : base(environment, phantomFileLocation, jasmineTestFileLocation, verbosityLevel, timeOut, null, null)
        {
            var jasmineConfigurationFileLocation = environment.GetJasmineConfigurationFileLocation();
            if (!File.Exists(jasmineConfigurationFileLocation))
            {
                throw new JasmineConfigurationFileDoesNotExistException();
            }
        }

        protected override string BuildArgs()
        {
            var runDir = environment.GetRunDir();
            var configText = File.ReadAllText(environment.GetJasmineConfigurationFileLocation());
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
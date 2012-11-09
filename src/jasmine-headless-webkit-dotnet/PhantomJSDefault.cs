using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace jasmine_headless_webkit_dotnet
{
    public class PhantomJSDefault : PhantomJSFromJSFiles
    {
        public PhantomJSDefault(ILocalEnvironment environment, string phantomFileLocation, string jasmineTestFileLocation, VerbosityLevel verbosityLevel, int timeOut)
            : base(environment, phantomFileLocation, jasmineTestFileLocation, verbosityLevel, timeOut, null, null) { }

        protected override string BuildArgs()
        {
            var runDir = environment.GetRunDir();
            var configPath = Path.Combine(runDir, "ScriptTests", "Support", "Jasmine.js");
            var configText = File.ReadAllText(configPath);
            //var config = configText.FromJson<JasmineConfig>();
            var config = JsonConvert.DeserializeObject<JasmineConfig>(configText);
            sourceFiles = config.src_files.SelectMany(f => Glob.GetMatches(CanonicalizePath(Path.Combine(runDir, config.src_dir, f)))).ToArray();
            testFiles = config.spec_files.SelectMany(f => Glob.GetMatches(CanonicalizePath(Path.Combine(runDir, config.spec_dir, f)))).ToArray();
            return base.BuildArgs();
        }

        private static string CanonicalizePath(string path)
        {
            return path.Replace('\\', '/');
        }

        private class JasmineConfig
        {
            public string[] src_files { get; set; }
            public string[] spec_files { get; set; }
            public string src_dir { get; set; }
            public string spec_dir { get; set; }
        }
    }
}
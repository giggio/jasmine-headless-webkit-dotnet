using System;
using System.IO;
using CoffeeSharp;

namespace jasmine_headless_webkit_dotnet
{
    public class SourceFile
    {
        public SourceFile(string path)
        {
            Path = path;
        }
        public string CompiledPath { get; private set; }
        public string Path { get; private set; }
        public bool IsCoffee()
        {
            return Path.EndsWith(".coffee", StringComparison.CurrentCultureIgnoreCase);
        }

        private static readonly CoffeeScriptEngine coffeeScriptEngine = new CoffeeScriptEngine();

        public virtual void Compile(string directory)
        {
            CreateCompiledPath(directory);
            WriteCoffeeScriptContentToFile();
        }

        private void WriteCoffeeScriptContentToFile()
        {
            using (var reader = new StreamReader(Path))
            {
                var compiledCoffeeScript = coffeeScriptEngine.Compile(reader.ReadToEnd());
                File.WriteAllText(CompiledPath, compiledCoffeeScript);
            }
        }

        protected void CreateCompiledPath(string directory)
        {
            if (!IsCoffee()) throw new InvalidOperationException("Can only compile coffeescript files.");
            var jsFileName = System.IO.Path.GetFileNameWithoutExtension(Path) + "-" + new Random().Next(0, 1000000) + "." + "js";
            CompiledPath = System.IO.Path.Combine(directory, jsFileName);
        }
    }
}
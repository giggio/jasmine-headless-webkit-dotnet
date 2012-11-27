using System;
using System.IO;
using CoffeeSharp;

namespace jasmine_headless_webkit_dotnet
{
    public class CoffeeScriptSourceCompiler : ISourceCompiler
    {
        private static readonly CoffeeScriptEngine coffeeScriptEngine = new CoffeeScriptEngine();

        public bool Accept(string file)
        {
            return !string.IsNullOrWhiteSpace(file) && file.EndsWith(".coffee", StringComparison.CurrentCultureIgnoreCase);
        }

        public string Compile(string file)
        {
            if (Accept(file))
            {
                if (!File.Exists(file))
                {
                    throw new Exception("attempt to compile a nonexistent file.");
                }

                using (var reader = new StreamReader(file))
                {
                    var compiledCoffeeScript = coffeeScriptEngine.Compile(reader.ReadToEnd());
                    return compiledCoffeeScript;
                }
            }
            throw new Exception("attempt to compile an invalid file.");
        }
    }
}
using System;
using System.ComponentModel.Composition;
using System.IO;
using CoffeeSharp;

namespace jasmine_headless_webkit_dotnet
{
    [Export(typeof(ISourceCompiler))]
    public class CoffeeScriptSourceCompiler : ISourceCompiler
    {
        private static readonly CoffeeScriptEngine CoffeeScriptEngine = new CoffeeScriptEngine();

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
                    return CoffeeScriptEngine.Compile(reader.ReadToEnd());
                }
            }
            throw new Exception("attempt to compile an invalid file.");
        }
    }
}
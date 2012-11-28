using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using TypeScript;
using jasmine_headless_webkit_dotnet;

namespace jasmine_headless_typeScript_compiler
{
    [Export(typeof(ISourceCompiler))]
    public class TypeScriptSourceCompiler : ISourceCompiler
    {
        private static readonly TypeScriptCompiler TypeScriptCompiler = new TypeScriptCompiler();

        public bool Accept(string file)
        {
            return !string.IsNullOrWhiteSpace(file) && file.EndsWith(".ts", StringComparison.CurrentCultureIgnoreCase);
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
                    return TypeScriptCompiler.Compile(reader.ReadToEnd());
                }
            }
            throw new Exception("attempt to compile an invalid file.");
        }
    }
}

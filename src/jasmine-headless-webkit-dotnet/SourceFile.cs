using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoffeeSharp;

namespace jasmine_headless_webkit_dotnet
{
    public class SourceFile
    {
        private List<ISourceCompiler> _compilers = new List<ISourceCompiler> {new CoffeeScriptSourceCompiler()};

        public bool IsAccepted()
        {
            return Path.EndsWith(".js", StringComparison.CurrentCultureIgnoreCase) || _compilers.Any(c => c.Accept(Path));
        }

        private string Compile()
        {
            if (IsAccepted())
            {
                return Path.EndsWith(".js", StringComparison.CurrentCultureIgnoreCase) 
                    ? File.ReadAllText(Path) 
                    : _compilers.FirstOrDefault(c => c.Accept(Path)).Compile(Path);
            }
            throw new Exception("attempt to compile an invalid file.");
        }

        public SourceFile(string path)
        {
            Path = path;
        }

        public string CompiledPath { get; private set; }
        public string Path { get; private set; }

        public virtual void Compile(string directory)
        {
            CreateCompiledPath(directory);
            WriteCompiledContentToFile();
        }

        private void WriteCompiledContentToFile()
        {
            File.WriteAllText(CompiledPath, Compile());
        }

        protected void CreateCompiledPath(string directory)
        {
            if (!IsAccepted()) throw new InvalidOperationException("Can only compile coffeescript files.");
            var jsFileName = System.IO.Path.GetFileNameWithoutExtension(Path) + "-" + new Random().Next(0, 1000000) + "." + "js";
            CompiledPath = System.IO.Path.Combine(directory, jsFileName);
        }
    }
}
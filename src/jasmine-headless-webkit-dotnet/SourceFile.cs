using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace jasmine_headless_webkit_dotnet
{
    public class SourceFile
    {
        [ImportMany(typeof(ISourceCompiler))]
        private IEnumerable<ISourceCompiler> compilers { get; set; }

        private CompositionContainer _container;

        public SourceFile()
        {
            var catalog = new AggregateCatalog();
            _container = new CompositionContainer(catalog);
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            if (AppDomain.CurrentDomain.ShadowCopyFiles)
                catalog.Catalogs.Add(new DirectoryCatalog(System.IO.Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath)));
            else
                catalog.Catalogs.Add(new DirectoryCatalog(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            var batch = new CompositionBatch();
            batch.AddPart(this); 

            try
            {
                this._container.Compose(batch); 
            }
            catch (CompositionException compositionException)
            {
                throw new Exception("Error loading souc compilers.", compositionException);
            }
        }

        public SourceFile(string path) : this()
        {
            Path = path;
        }

        public bool IsAccepted()
        {
            return Path.EndsWith(".js", StringComparison.CurrentCultureIgnoreCase) || compilers.Any(c => c.Accept(Path));
        }

        private string Compile()
        {
            if (IsAccepted())
            {
                return Path.EndsWith(".js", StringComparison.CurrentCultureIgnoreCase) 
                    ? File.ReadAllText(Path) 
                    : compilers.FirstOrDefault(c => c.Accept(Path)).Compile(Path);
            }
            throw new Exception("attempt to compile an invalid file.");
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
            if (!IsAccepted()) throw new InvalidOperationException("attempt to compile an invalid file.");
            var jsFileName = System.IO.Path.GetFileNameWithoutExtension(Path) + "-" + new Random().Next(0, 1000000) + "." + "js";
            CompiledPath = System.IO.Path.Combine(directory, jsFileName);
        }
    }
}
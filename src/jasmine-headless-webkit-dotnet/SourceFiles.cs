using System.Collections.Generic;
using System.Linq;

namespace jasmine_headless_webkit_dotnet
{
    public class SourceFiles 
    {
        public SourceFiles(IEnumerable<string> files)
        {
            sourceFiles = CreateSourceFiles(files);
        }

        protected virtual List<SourceFile> CreateSourceFiles(IEnumerable<string> files)
        {
            return new List<SourceFile>(files.Select(f => new SourceFile(f)));
        }

        private readonly List<SourceFile> sourceFiles; 
        public string[] Compile(string directory)
        {
            var files = sourceFiles.ToArray();
            var jsFiles = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if (!file.IsAccepted())
                {
                    jsFiles[i] = file.Path;
                    continue;
                }
                file.Compile(directory);
                jsFiles[i] = file.CompiledPath;
            }
            return jsFiles;
        }
    }
}
using System;
using System.ComponentModel;
using Args;
using Args.Help;
using Args.Help.Formatters;

namespace jasmine_headless_webkit_dotnet
{
    [Description("Jasmine Runner based on .NET")]
    public class Arguments
    {
        public Arguments()
        {
            VerbosityLevel = VerbosityLevel.Normal;
        }
        [Description("This help text.")]
        [ArgsMemberSwitch("Help", "H", "?")]
        public bool Help { get; set; }
        [Description("The test archive.")]
        public string FileName { get; set; }
        [Description("The time to wait for the tests to complete before stopping.")]
        [ArgsMemberSwitch("Timeout")]
        public int? Timeout { get; set; }
        [Description("The verbosity level. Defaults to normal. Possible values: normal, verbose.")]
        public VerbosityLevel VerbosityLevel { get; set; }
        [Description("Javascript files to test.")]
        [ArgsMemberSwitch("TestFiles", "T")]
        public string[] TestFiles { get; set; }
        [Description("Javascript source files.")]
        public string[] SourceFiles { get; set; }
        [Description("Configuration file.")]
        public string ConfigFile { get; set; }
        
        public RunType RunType
        {
            get
            {
                if (IsHelpRun())
                {
                    return RunType.Help;
                }
                if (IsJsRun())
                {
                    return RunType.JSFiles;
                }
                if (IsHtmlRun())
                {
                    return RunType.HtmlFile;
                }
                if (IsConfigFileRun())
                {
                    return RunType.ConfigFile;
                }
                if (IsDefaultRun())
                {
                    return RunType.Default;
                }
                return RunType.Help;
            }
        }

        private bool IsConfigFileRun()
        {
            return !string.IsNullOrWhiteSpace(ConfigFile);
        }

        private bool IsDefaultRun()
        {
            return string.IsNullOrEmpty(FileName) &&
                   (TestFiles == null || TestFiles.Length == 0) && (SourceFiles == null || SourceFiles.Length == 0) &&
                   !Help;
        }

        private bool IsHtmlRun()
        {
            return !string.IsNullOrEmpty(FileName);
        }

        private bool IsHelpRun()
        {
            return Help;
        }

        private bool IsJsRun()
        {
            return ((TestFiles != null && TestFiles.Length > 0) && (SourceFiles != null && SourceFiles.Length > 0));
        }

        public int GetTimeOut()
        {
            return Timeout ?? 60;
        }

        public static void WriteHelp()
        {
            var help = new HelpProvider().GenerateModelHelp(Configuration.Configure<Arguments>());
            var f = new ConsoleHelpFormatter(Console.BufferWidth, 1, 5);
            Console.WriteLine(f.GetHelp(help));
        }
    }
}
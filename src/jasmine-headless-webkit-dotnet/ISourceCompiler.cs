namespace jasmine_headless_webkit_dotnet
{
    public interface ISourceCompiler
    {
        bool Accept(string file);
        string Compile(string file);
    }
}
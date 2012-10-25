using System;
using System.IO;
using CassiniDev;

namespace jasmine_headless_webkit_dotnet
{
    public class WebServer : IWebServer
    {
        private Server server;

        public void RunServer(string directory, int port)
        {
            if (!Directory.Exists(directory))
                throw new DirectoryNotFoundException(string.Format("Directory '{0}' not found.", directory));
            server = new Server(port, directory);
        }

        public void Dispose()
        {
            DisposeServer();
        }

        private void DisposeServer()
        {
            if (server != null)
                server.Dispose();
        }
    }

    public interface IWebServer : IDisposable
    {
        void RunServer(string directory, int port);
    }
}
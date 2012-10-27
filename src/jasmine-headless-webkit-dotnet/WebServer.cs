using System;
using System.IO;
using CassiniDev;

namespace jasmine_headless_webkit_dotnet
{
    public class WebServer : IWebServer
    {
        private readonly bool runServer;
        private readonly string directory;
        private readonly int port;
        private Server server;

        public WebServer(bool runServer, string directory, int port)
        {
            this.runServer = runServer;
            this.directory = directory;
            this.port = port;
        }

        public void RunServer()
        {
            if (!runServer) return;
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
        void RunServer();
    }
}
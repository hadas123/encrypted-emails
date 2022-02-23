using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Mailbox
{
    class Conect
    {
        public static string redirectUri;
        public static HttpListener http;
        static public void conect()
        {
            // Creates a redirect URI using an available port on the loopback address.
            redirectUri = string.Format("http://127.0.0.1:{0}/", GetRandomUnusedPort());
            Console.WriteLine("redirect URI: " + redirectUri);

            // Creates an HttpListener to listen for requests on that redirect URI.
            http = new HttpListener();
            http.Prefixes.Add(redirectUri);
            Console.WriteLine("Listening ...");
            http.Start();

        }
        static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

    }
}

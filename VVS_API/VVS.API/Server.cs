using System;
using System.IO;
using System.Net;

namespace VVS.API
{
    class Server
    {
        private readonly HttpListener server;

        public Server()
        {
            server = new HttpListener();
        }
    }
}

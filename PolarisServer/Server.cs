﻿using System;
using System.Collections.Generic;

using PolarisServer.Network;

namespace PolarisServer
{
    public class Server
    {
        private List<Client> _clients;
        private Network.SocketServer _server;

        public List<Client> Clients { get { return _clients; } }
        public static Server Instance { get; private set; }
        public DateTime StartTime { get; private set; }

        public Server()
        {
            _clients = new List<Client>();
            _server = new Network.SocketServer(12205);
            _server.NewClient += HandleNewClient;
            Instance = this;
            StartTime = DateTime.Now;

            new QueryServer(QueryMode.BlockBalance, 12200);
        }

        public void Run()
        {
            _server.Run();
        }

        void HandleNewClient(SocketClient client)
        {
            var c = new Client(this, client);
            _clients.Add(c);
        }
    }
}

using AbiaServer.models;
using DarkRift.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiaServer
{
    public class ClientRegistry
    {
        private Dictionary<int, TempUser> _clientIDToPlayer = new Dictionary<int, TempUser>();

        public void RegisterClient(ClientConnectedEventArgs e)
        {
            if (!_clientIDToPlayer.ContainsKey(e.Client.ID))
            {
                TempUser user = new TempUser();
                user.client = e.Client;
                _clientIDToPlayer.Add(e.Client.ID, user);
            }
        }

        public void UnregisterClient(ClientDisconnectedEventArgs e)
        {
            if (_clientIDToPlayer.ContainsKey(e.Client.ID))
                _clientIDToPlayer.Remove(e.Client.ID);
        }
    }
}

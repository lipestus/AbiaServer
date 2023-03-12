using System;
using DarkRift.Server;
using DarkRift;
using AbiaServerModels;
using AbiaServerModels.NetworkData;
using System.Collections.Generic;
using AbiaServer.models;

namespace AbiaServer
{
    public class AbiaServer : Plugin
    {
        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);
        public Dictionary<int, User> clientIDToPlayer = new Dictionary<int, User>();
        public AbiaServer(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            Console.WriteLine("Server is listening...");
            ClientManager.ClientConnected += ClientManager_ClientConnected;
            ClientManager.ClientDisconnected += ClientManager_ClientDisconnected;
        }

        private void ClientManager_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Console.WriteLine("Player Connected...");
            e.Client.MessageReceived += OnMessageReceived;
            RegisterClient(e);
        }
        private void ClientManager_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Player Disconnected...");
            e.Client.MessageReceived -= OnMessageReceived;
            UnregisterClient(e);
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            using Message message = e.GetMessage();
            using DarkRiftReader reader = message.GetReader();
            switch (message.Tag)
            {
                case (ushort)Tags.TagType.TEST:
                    Chat chat = reader.ReadSerializable<Chat>();
                    Console.WriteLine(chat.chatMessage);
                    break;
                case (ushort)Tags.TagType.USER_LOGIN:
                    UserLogin userLogin = reader.ReadSerializable<UserLogin>();
                    if (clientIDToPlayer.ContainsKey(e.Client.ID))
                    {
                        User user = clientIDToPlayer[e.Client.ID];
                        user.accountName = userLogin.accountName;
                        // pass the password here as well
                    }

                    //OutputConnectedPlayers(clientIDToPlayer);
                    break;
            }
        }

        private void RegisterClient(ClientConnectedEventArgs e)
        {
            if (!clientIDToPlayer.ContainsKey(e.Client.ID))
            {
                User user = new User();
                user.client = e.Client;
                clientIDToPlayer.Add(e.Client.ID, user);
            }
        }

        private void UnregisterClient(ClientDisconnectedEventArgs e)
        {
            if (clientIDToPlayer.ContainsKey(e.Client.ID))
                clientIDToPlayer.Remove(e.Client.ID);
        }

        private void OutputConnectedPlayers(Dictionary<int, User> users)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine($"{users.Count} users");
            foreach (KeyValuePair<int, User> pair in users)
            {
                Console.WriteLine(pair.Key.ToString() + ": " + pair.Value.accountName);
            }
            Console.WriteLine("---------------------------");
        }

    }
}

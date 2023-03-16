using System;
using DarkRift.Server;
using DarkRift;
using AbiaServerModels;
using AbiaServerModels.NetworkData;
using System.Collections.Generic;
using AbiaServer.models;
using SQLConnector.DbAccess;
using SQLConnector.Data;
using SQLConnector.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Reflection.Emit;

namespace AbiaServer
{
    public class AbiaServer : Plugin
    {
        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);

        private ConfigurationLoader _configLoader;
        private DatabaseLoader _databaseLoader;
        private AccountManager _accountManager;
        private ClientRegistry _clientRegistry;
        public AbiaServer(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            try
            {
                _configLoader = new ConfigurationLoader();
            }
            catch (Exception ex)
            {
                Logger.Error($"An exception occurred: {ex.Message}", ex);
            }

            InitialiseListeners();
            _databaseLoader = new DatabaseLoader(_configLoader.Config);
            _accountManager = new AccountManager(_databaseLoader.UserData);
            _clientRegistry = new ClientRegistry();
        }

        private void InitialiseListeners()
        {
            ClientManager.ClientConnected += ClientManager_ClientConnected;
            ClientManager.ClientDisconnected += ClientManager_ClientDisconnected;
            Console.WriteLine("Server is listening...");
        }

        private void ClientManager_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Console.WriteLine("Player Connected...");
            e.Client.MessageReceived += OnMessageReceived;
            _clientRegistry.RegisterClient(e);
        }
        private void ClientManager_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Player Disconnected...");
            e.Client.MessageReceived -= OnMessageReceived;
            _clientRegistry.UnregisterClient(e);
        }

        private async void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            using Message message = e.GetMessage();
            using DarkRiftReader reader = message.GetReader();
            switch (message.Tag)
            {
                case (ushort)Tags.MessageTypes.CreateAccountRequest:
                    await _accountManager.OnCreateAccountRequest(e);
                    break;
                case (ushort)Tags.MessageTypes.LoginRequest:
                    await _accountManager.OnLoginRequest(e);
                    break;
            }
        }
    }
}

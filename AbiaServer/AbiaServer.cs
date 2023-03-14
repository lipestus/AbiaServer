using System;
using DarkRift.Server;
using DarkRift;
using AbiaServerModels;
using AbiaServerModels.NetworkData;
using System.Collections.Generic;
using AbiaServer.models;
using System.Reflection;
using System.IO;
using SQLConnector.DbAccess;
using SQLConnector.Data;
using SQLConnector.Models;
using Microsoft.Extensions.Configuration;

namespace AbiaServer
{
    public class AbiaServer : Plugin
    {
        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);
        public Dictionary<int, TempUser> clientIDToPlayer = new Dictionary<int, TempUser>();

        private ISqlDataAccess _db;
        private UserData _userData;
        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AbiaServerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public AbiaServer(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            try
            {
                InitialiseConfigBuilder();
            }
            catch (Exception ex)
            {
                // Log the exception using the Logger class
                Logger.Error($"An exception occurred: {ex.Message}", ex);
            }

            InitialiseListeners();
        }

        private void InitialiseConfigBuilder()
        {
            var config = new ConfigurationBuilder()
                 .AddInMemoryCollection(new Dictionary<string, string>
                 {
                    { "ConnectionStrings:MyConnectionString", _connectionString }
                 })
                 .Build();
            _db = new SqlDataAccess(config);
            _userData = new UserData(_db);
        }

        private void InitialiseListeners()
        {
            ClientManager.ClientConnected += ClientManager_ClientConnected;
            ClientManager.ClientDisconnected += ClientManager_ClientDisconnected;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Console.WriteLine("Server is listening...");
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
                case (ushort)Tags.TagType.CREATE_USER:
                    UserLogin userLogin = reader.ReadSerializable<UserLogin>();
                    if (clientIDToPlayer.ContainsKey(e.Client.ID))
                    {
                        UserModel userModel = new UserModel(userLogin.accountName, userLogin.password);
                        _userData.InsertUser(userModel);
                        Console.WriteLine($"User created : {userModel.AccountName}");
                    }
                    break;
            }
        }

        private void RegisterClient(ClientConnectedEventArgs e)
        {
            if (!clientIDToPlayer.ContainsKey(e.Client.ID))
            {
                TempUser user = new TempUser();
                user.client = e.Client;
                clientIDToPlayer.Add(e.Client.ID, user);
            }
        }

        private void UnregisterClient(ClientDisconnectedEventArgs e)
        {
            if (clientIDToPlayer.ContainsKey(e.Client.ID))
                clientIDToPlayer.Remove(e.Client.ID);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name).Name;
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assemblyPath = Path.Combine(dir, $"{assemblyName}.dll");
            if (File.Exists(assemblyPath))
            {
                return Assembly.LoadFrom(assemblyPath);
            }
            return null;
        }
    }
}

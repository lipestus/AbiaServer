using AbiaServerModels.NetworkData;
using AbiaServerModels;
using DarkRift;
using DarkRift.Server;
using SQLConnector.Data;
using SQLConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiaServer
{
    public class AccountManager
    {
        private UserData _userData;

        public AccountManager(UserData userData)
        {
            _userData = userData;
        }

        public async Task OnCreateAccountRequest(MessageReceivedEventArgs e)
        {
            // Deserialize the request
            using Message message = e.GetMessage();
            using DarkRiftReader reader = message.GetReader();
            CreateAccountRequest accountToCreate = reader.ReadSerializable<CreateAccountRequest>();

            // Validate the account information
            if (string.IsNullOrEmpty(accountToCreate.accountName) || string.IsNullOrEmpty(accountToCreate.password))
            {
                var response = DarkRift.Message.Create((ushort)Tags.MessageTypes.CreateAccountResponse, new CreateAccountResponse { Success = false });
                e.Client.SendMessage(response, SendMode.Reliable);
                return;
            }

            // Create a new account in the database
            UserModel userModel = new UserModel(accountToCreate.accountName, accountToCreate.password);
            if (userModel.IsAccountNameValid)
            {
                await _userData.InsertUser(userModel);
            }
            else
            {
                var response = DarkRift.Message.Create((ushort)Tags.MessageTypes.CreateAccountResponse, new CreateAccountResponse { Success = false });
                e.Client.SendMessage(response, SendMode.Reliable);
                return;
            }

            // Respond with a success message
            var successResponse = DarkRift.Message.Create((ushort)Tags.MessageTypes.CreateAccountResponse, new CreateAccountResponse { Success = true });
            e.Client.SendMessage(successResponse, SendMode.Reliable);
        }

        public async Task OnLoginRequest(MessageReceivedEventArgs e)
        {
            // Deserialize the request
            using Message message = e.GetMessage();
            using DarkRiftReader reader = message.GetReader();
            LoginRequest loginRequest = reader.ReadSerializable<LoginRequest>();

            // Validate the login information
            if (string.IsNullOrEmpty(loginRequest.AccountName) || string.IsNullOrEmpty(loginRequest.Password))
            {
                var response = DarkRift.Message.Create((ushort)Tags.MessageTypes.LoginResponse, new LoginResponse { Success = false });
                e.Client.SendMessage(response, SendMode.Reliable);
                return;
            }

            // Check if the account exists
            UserModel userFromDB = await _userData.GetUserByName(loginRequest.AccountName);

            if (userFromDB != null)
            {
                UserModel existentUser = new UserModel(userFromDB.AccountName, loginRequest.Password);
                if (existentUser.VerifyPassword(loginRequest.Password))
                {
                    var successResponse = DarkRift.Message.Create((ushort)Tags.MessageTypes.LoginResponse, new LoginResponse 
                    {   Id = userFromDB.Id,
                        Success = true 
                    });
                    e.Client.SendMessage(successResponse, SendMode.Reliable);
                }
                else
                {
                    var response = DarkRift.Message.Create((ushort)Tags.MessageTypes.LoginResponse, new LoginResponse { Success = false });
                    e.Client.SendMessage(response, SendMode.Reliable);
                    return;
                }
            }
            else
            {
                var response = DarkRift.Message.Create((ushort)Tags.MessageTypes.LoginResponse, new LoginResponse { Success = false });
                e.Client.SendMessage(response, SendMode.Reliable);
                return;
            }
        }
    }
}

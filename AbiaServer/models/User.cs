using DarkRift.Server;
using System;

namespace AbiaServer.models
{
    public class User
    {
        public String accountName { get; set; }
        public String password { get; set; }
        public IClient client { get; set; }
    }
}

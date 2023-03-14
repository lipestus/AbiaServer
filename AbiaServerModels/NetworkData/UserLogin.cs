using DarkRift;
using System;

namespace AbiaServerModels.NetworkData
{
    public class UserLogin : IDarkRiftSerializable
    {
        public int Id { get; set; }
        public String accountName { get; set; }
        public string password { get; set; }
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadInt32();
            accountName = e.Reader.ReadString();
            password = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(accountName);
            e.Writer.Write(password);
        }
    }
}

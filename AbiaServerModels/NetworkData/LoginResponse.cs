using DarkRift;
using System;

namespace AbiaServerModels.NetworkData
{
    public class LoginResponse : IDarkRiftSerializable
    {
        public int Id { get; set; }
        public bool Success { get; set; }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadInt32();
            Success = e.Reader.ReadBoolean();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Success);
        }
    }
}

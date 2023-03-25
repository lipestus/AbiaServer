using DarkRift;
using System;

namespace AbiaServerModels.NetworkData
{
    public class WorldResponse : IDarkRiftSerializable
    {
        public string Name { get; set; }
        public long ip { get; set; }
        public int Port { get; set; }
        public void Deserialize(DeserializeEvent e)
        {
            Name = e.Reader.ReadString();
            ip = e.Reader.ReadInt64();
            Port = e.Reader.ReadInt32();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Name);
            e.Writer.Write(ip);
            e.Writer.Write(Port);
        }
    }
}

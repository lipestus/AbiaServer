using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiaServerModels.NetworkData
{
    public class Chat : IDarkRiftSerializable
    {
        public String chatMessage;
        public void Deserialize(DeserializeEvent e)
        {
            chatMessage = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(chatMessage);
        }
    }
}

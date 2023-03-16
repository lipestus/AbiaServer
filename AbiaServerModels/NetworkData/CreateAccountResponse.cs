using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiaServerModels
{
    public class CreateAccountResponse : IDarkRiftSerializable
    {
        public bool Success { get; set; }

        public void Deserialize(DeserializeEvent e)
        {
            Success = e.Reader.ReadBoolean();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Success);
        }
    }
}

using DarkRift;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiaServerModels.NetworkData
{
    public class LoginRequest : IDarkRiftSerializable
    {
        public string AccountName { get; set; }
        public string Password { get; set; }

        public void Deserialize(DeserializeEvent e)
        {
            AccountName = e.Reader.ReadString();
            Password = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(AccountName);
            e.Writer.Write(Password);
        }
    }
}

using DarkRift;

namespace AbiaServerModels.NetworkData
{
    public class LoginResponse : IDarkRiftSerializable
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

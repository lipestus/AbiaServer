using DarkRift;

namespace AbiaServerModels.NetworkData
{
    public class WorldListResponse : IDarkRiftSerializable
    {
        public WorldResponse[] Worlds { get; set; }

        public void Deserialize(DeserializeEvent e)
        {
            int worldCount = e.Reader.ReadInt32();
            Worlds = new WorldResponse[worldCount];
            for (int i = 0; i < worldCount; i++)
            {
                Worlds[i] = new WorldResponse();
                Worlds[i].Deserialize(e);
            }
        }

        public void Serialize(SerializeEvent e)
        {
            if (Worlds != null)
            {
                e.Writer.Write(Worlds.Length);
                foreach (var world in Worlds)
                {
                    world.Serialize(e);
                }
            }
            else
            {
                e.Writer.Write(0);
            }
        }
    }
}

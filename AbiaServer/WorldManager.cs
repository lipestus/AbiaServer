using AbiaServerModels;
using AbiaServerModels.NetworkData;
using DarkRift;
using DarkRift.Server;
using SQLConnector.Data;
using SQLConnector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AbiaServer
{
    public class WorldManager
    {
        private WorldData _worldData;
        public WorldManager(WorldData worldData) 
        {
            _worldData = worldData;
        }

        public async Task OnWorldRequest(MessageReceivedEventArgs e)
        {
            // Deserialize the request
            using Message message = e.GetMessage();
            using DarkRiftReader reader = message.GetReader();

            // Get the list of available worlds
            IEnumerable<WorldModel> worlds = await _worldData.GetWorlds();

            // Convert the list of worlds to a WorldModel object for each world
            List<WorldResponse> worldResponses = new List<WorldResponse>();

            foreach (var world in worlds)
            {
                worldResponses.Add(new WorldResponse
                {
                    Name = world.Name,
                    ip = long.Parse(world.ip),
                    Port = world.Port
                });
            }

            // Create a response message with the list of worlds
            WorldListResponse response = new WorldListResponse { Worlds = worldResponses.ToArray() };
            using Message responseMessage = Message.Create((ushort)Tags.MessageTypes.WorldResponse, response);
            e.Client.SendMessage(responseMessage, SendMode.Reliable);
        }
    }
}

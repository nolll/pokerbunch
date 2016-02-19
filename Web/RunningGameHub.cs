using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
namespace Web
{
    public class RunningGameHub : Hub
    {
        public void DataUpdated(string slug, int playerId)
        {
            SendMessage(slug, playerId);
        }

        public async Task JoinGame(string slug)
        {
            await Groups.Add(Context.ConnectionId, slug);
        }

        private void SendMessage(string slug, string message)
        {
            Clients.Group(slug).updateClient(message);
        }

        private void SendMessage(string slug, int playerId)
        {
            Clients.Group(slug).updateClient(playerId);
        }
    }
}
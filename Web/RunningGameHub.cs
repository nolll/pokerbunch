using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
namespace Web
{
    public class RunningGameHub : Hub
    {
        public void DataUpdated(string slug, string message)
        {
            var responseMessage = $"{message}, sent from server";
            SendMessage(slug, responseMessage);
        }

        public async Task JoinGame(string slug)
        {
            await Groups.Add(Context.ConnectionId, slug);
            SendMessage(slug, Context.User.Identity.Name + " joined");
            SendMessage("other game", "message to dev null");
        }

        private void SendMessage(string slug, string message)
        {
            Clients.Group(slug).updateClient(message);
        }
    }
}
using Microsoft.AspNet.SignalR;
namespace Web
{
    public class RunningGameHub : Hub
    {
        public void DataUpdated(string message)
        {
            Clients.All.updateClient(message);
        }
    }
}
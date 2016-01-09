using Microsoft.AspNet.SignalR;
namespace Web
{
    public class RunningGameHub : Hub
    {
        public void DataUpdated(string message)
        {
            var responseMessage = $"{message}, sent from server";
            Clients.All.updateClient(responseMessage);
        }
    }
}
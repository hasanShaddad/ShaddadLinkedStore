using Microsoft.AspNet.SignalR;

namespace Elmatgar
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string connectionid)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message, connectionid);
        }
        public void connectMessage(string name, string message, string connectionid)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addconnMessage(name, message, connectionid);
        }
        public void desconnectMessage(string name, string message, string connectionid)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addconnMessage(name, message, connectionid);
        }

    }
}
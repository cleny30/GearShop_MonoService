using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
namespace GearShopWeb.SingalR
{
    public class SignalRHub:Hub
    {
        private static ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            // You can get the username from the query string or from the context
            // Here I'm assuming the username is passed in the query string
            var username = Context.GetHttpContext().Request.Query["username"];

            if (!string.IsNullOrEmpty(username))
            {
                _userConnections[username] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var username = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(username))
            {
                _userConnections.TryRemove(username, out _);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task LoadOrder(string username)
        {
            if (_userConnections.TryGetValue(username, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("LoadOrder");
            }
        }
    }
}

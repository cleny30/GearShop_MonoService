using DataAccess.Service;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace GearShopWeb
{
    public class SignalRServer : Hub
    {
        private static ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();
        private readonly IHttpContextAccessor _contx;
        private readonly CartService cartService;
        private readonly HeaderService headerService;

        public SignalRServer(IHttpContextAccessor contx, CartService cartService, HeaderService headerService)
        {
            _contx = contx;
            this.cartService = cartService;
            this.headerService = headerService;
        }

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

        public async Task LoadCartData()
        {
            var userSession = _contx.HttpContext.Session.GetString("username");
            if (_userConnections.TryGetValue(userSession, out var connectionId))
            {
                int count = cartService.GetCartsByUserName(userSession).Count();
                await Clients.Client(connectionId).SendAsync("ReceiveLoadCardData", count);
            }
        }
    }
}

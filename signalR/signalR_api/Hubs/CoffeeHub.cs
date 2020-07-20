using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace signalR_api
{
    [Authorize]
    public class CoffeeHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("wpiete");
        }
    }
}
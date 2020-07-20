using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using signalR_api.Models;

namespace signalR_api 
{
    class RestaurantHub : Hub 
    {
        public RestaurantHub()
        {
        }

        
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("wpiete");
            
        }

        private void CreateGroup()
        {
         //   Context.User.
           // Groups.AddToGroupAsync(Clients.Caller, "")
        }

        private void JoinGroup()
        {
            
        }

        
        public async Task MealPrepared(string message)
        {
            if(message == "")
                await Clients.Caller.SendAsync("MessageReciver", $"You have sent: an empty message to the server fool.");
            else
                await Clients.Caller.SendAsync("MessageReciver", $"You have sent: '{message}' to the server fool.");
        }

        public async Task PersonSended(User person)
        {
            var czlowiekString = person.ToString();
          //  await SendMeAMessage(czlowiekString);
            await Clients.All.SendAsync("MessageReciver", person);
            // await Clients.All.SendAsync("MessageReciver", $"You have sent: '{message}' to the server fool.");
        }
    }
}
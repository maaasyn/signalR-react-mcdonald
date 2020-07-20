using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using signalR_api.Dtos;
using signalR_api.Helpers;
using signalR_api.Models;

namespace signalR_api
{
    [Authorize]
    class ChatHub : Hub
    {
        private readonly IMapper _mapper;

        private readonly IKolejka _kolejka;
        // private readonly ITest _test;

        public ChatHub(IMapper mapper, IKolejka kolejka)
        {
            _mapper = mapper;
            _kolejka = kolejka;
        }

        public override async Task OnConnectedAsync()
        {
            var groupName = GetGroupName();
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.All.SendAsync("wpiete", $"{groupName} {Context.ConnectionId}");
            _kolejka.Add(groupName);
        }

        [Authorize(Roles = "Pos")]
        public async Task NewOrderSubmitter(string message)
        {
            var display = GroupName("display");
            var kitchen = GroupName("kitchen");
            var pos = GroupName("pos");


            var liczbaZKolejki = _kolejka.GetNextNumber(pos);

            var orderObject = new Order() {Id = 1, Product = message, OrderNumber = liczbaZKolejki};

            var orderForDisplay = _mapper.Map<Order, OrderForDisplayDto>(orderObject);
            var orderForKitchen = _mapper.Map<Order, OrderForKitchenDto>(orderObject);

            //TODO Insert payload into db

            await Clients.Group(kitchen).SendAsync("MessageReceiver", orderForDisplay);
            await Clients.Group(pos).SendAsync("MessageReceiver", orderObject);
            await Clients.Group(display).SendAsync("MessageReceiver", orderForDisplay);
        }

        [Authorize(Roles = "Pos, Kitchen")]
        public async Task OrderFinishedByKitchen(Order orderFromKitchen)
        {
            var kitchen = GroupName("kitchen");
            var pos = GroupName("pos");
            
            var orderFinishedByKitchen = _mapper.Map<Order, OrderForKitchenDto>(orderFromKitchen);

            // var orderObject = new Order() {Id = 1, Product = message, OrderNumber = liczbaZKolejki};

            await Clients.Group(pos).SendAsync("KichenOrderFinished", orderFinishedByKitchen);
            await Clients.Group(kitchen).SendAsync("KichenOrderFinished", orderFinishedByKitchen);
        }

        [Authorize(Roles = "Pos")]
        public async Task SetOrderForCollection(Order order)
        {
            var display = GroupName("display");
            var pos = GroupName("pos");
            
            await Clients.Group(pos).SendAsync("OrderReadyForCollection", order);
            await Clients.Group(display).SendAsync("OrderReadyForCollection", order);
        }
        
        [Authorize(Roles = "Pos")]
        public async Task OrderCollected(Order order)
        {
            var display = GroupName("display");
            var pos = GroupName("pos");
            
            
            
            await Clients.Group(pos).SendAsync("OrderCollected", order);
            await Clients.Group(display).SendAsync("OrderCollected", order);
            await Clients.Groups(display).SendAsync("OrderCollected", order);
        }


        //private methods
        private string GroupName(string deviceType)
        {
            var sessionId = Context.User.FindFirst(c => c.Type == "sessionId").Value;
            var groupName = sessionId + deviceType;
            return groupName;
        }

        private string GetGroupName()
        {
            var sessionId = Context.User.FindFirst(c => c.Type == "sessionId").Value;
            var deviceType = Context.User.FindFirst(c => c.Type == "device").Value;
            var groupName = sessionId + deviceType;
            return groupName;
        }

        private IList<string> GroupsToInform(params string[] groups)
        {
            var lista = new List<string>{};
            foreach (var item in groups)
            {
                lista.Add(GroupName(item)); 
            }

            return lista;
        }
    }
}
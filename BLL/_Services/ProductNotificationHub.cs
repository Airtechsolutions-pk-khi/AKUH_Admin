using Microsoft.AspNetCore.SignalR;
using MohsinFoodAdmin.Interface;
using System.Threading.Tasks;
using System;

namespace MohsinFoodAdmin.BLL._Services
{
	public class ProductNotificationHub : Hub
	{
		public void GetDataFromClient(string userId, string connectionId)
		{
			Clients.Client(connectionId).SendAsync("clientMethodName", $"Updated userid {userId}");
		}

		public override Task OnConnectedAsync()
		{
			var connectionId = Context.ConnectionId;
			Clients.Client(connectionId).SendAsync("WelcomeMethodName", connectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			var connectionId = Context.ConnectionId;
			return base.OnDisconnectedAsync(exception);
		}
	}
}

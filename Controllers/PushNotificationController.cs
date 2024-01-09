using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MohsinFoodAdmin._Models;
using MohsinFoodAdmin.BLL._Services;

namespace MohsinFoodAdmin.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors("CorsPolicy")]
	public class PushNotificationController : ControllerBase
	{
		private IHubContext<ProductNotificationHub> _hub;
		public PushNotificationController(IHubContext<ProductNotificationHub> hub)
		{
			_hub = hub;
		}

		/// <summary>
		/// Send message to all
		/// </summary>
		/// <param name="message"></param>
		[HttpPost]
		[Route("public")]
		public void Post([FromBody] Notification content)
		{
			_hub.Clients.All.SendAsync("publicMessageMethodName", content);
		}

		/// <summary>
		/// Send message to specific client
		/// </summary>
		/// <param name="connectionId"></param>
		/// <param name="message"></param>
		/// 
		[HttpPost("{connectionId}/{message}")]
		public void Post(string connectionId, string message)
		{
			_hub.Clients.Client(connectionId).SendAsync("privateMessageMethodName", message);
		}
	}
}

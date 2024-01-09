using Microsoft.AspNetCore.SignalR;
using MohsinFoodAdmin._Models;
using MohsinFoodAdmin.Interface;
using System.Threading.Tasks;

namespace MohsinFoodAdmin.BLL._Services
{
	public class InMemoryProductService
	{

		//private readonly IHubContext<ProductNotificationHub, INotificationHub> _productNotification;

		//public InMemoryProductService(IHubContext<ProductNotificationHub, INotificationHub> hubContext)
		//{
		//	_productNotification = hubContext;
		//}
		//public async Task UpdateProduct(string message)
		//{
		//	await _productNotification.Clients.All.SendMessage(new Notification
		//	{
		//		ProductID = "CheckID",
		//		ProductName = "Check",
		//		Message = message
		//	});
		//}
	}
}

using MohsinFoodAdmin._Models;
using System.Threading.Tasks;

namespace MohsinFoodAdmin.Interface
{
	public interface INotificationHub
	{

		public Task SendAsync(Notification notification);
	}
}

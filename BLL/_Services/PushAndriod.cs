using AKU_Admin.GlobalAndCommons;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKU_Admin.BLL._Services
{
	public static class PushAndriod
	{
		public static async Task PushNotify(string title, string body)
		{
			{
				using (var client = new HttpClient())
				{
					var URL = string.Format("{0}/api/push/androidData/{1}/{2}", AppGlobals.PublishedURL, title, body);
					var response = await client.GetAsync(URL);
					response.EnsureSuccessStatusCode();
				}
			}
		}
	}
}

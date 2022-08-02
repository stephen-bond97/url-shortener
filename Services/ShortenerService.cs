using System.Text;
using UrlShortener.Models;

namespace UrlShortener.Services
{
	public class ShortenerService: IShortenerService
	{
		public async Task<string> GetShortUrl(string url)
		{
			string response = await ReadFromDB(url);

			return response;
		}

		private async Task<string> ReadFromDB(string url)
		{
			using (var db = new entitycoreContext())
			{
				var newItem = new Item();
				newItem.Name = url;
				newItem.Description = $"This is a url {url}";
				db.Items.Add(newItem);

				var count = await db.SaveChangesAsync();

				StringBuilder sb = new StringBuilder();

				sb.AppendLine($"{count} records saved to database.");
				sb.AppendLine("All items in the databse: ");
				foreach (var item in db.Items)
				{
					sb.AppendLine($"{item.Name}, {item.Description}");
				}

				return sb.ToString();
			}
		}
	}
}

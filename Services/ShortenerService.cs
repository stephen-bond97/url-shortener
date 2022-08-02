using System.Text;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Services
{
	public class ShortenerService: IShortenerService
	{
		private readonly IShortUrlRepository _repo;

		public ShortenerService(IShortUrlRepository repo)
		{
			this._repo = repo;
		}

		public async Task<string> InsertShortUrl(string url)
		{
			var id = await this._repo.InsertShortUrl(url);
			var encodedid = Encoding.UTF8.GetBytes(id);

			return Convert.ToBase64String(encodedid);
		}

		public async Task<string?> GetShortUrl(string encodedId)
		{
			byte[] idBytes = Convert.FromBase64String(encodedId);
			string decodedString = Encoding.UTF8.GetString(idBytes);
			if (int.TryParse(decodedString, out int id))
			{
				var url = await this._repo.GetUrl(id);
				return url?.Url;
			}
			return null;
		}
	}
}

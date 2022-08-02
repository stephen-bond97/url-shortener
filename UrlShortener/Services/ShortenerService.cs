using Base62;
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

		public async Task<string?> InsertShortUrl(string url)
		{
			if (Uri.TryCreate(url, UriKind.Absolute, out _))
			{
				var converter = new Base62Converter();

				var id = await this._repo.InsertShortUrl(url);

				return converter.Encode(id.ToString());
			}

			return null;			
		}

		public async Task<string?> GetShortUrl(string encodedId)
		{
			var converter = new Base62Converter();

			string decodedString = converter.Decode(encodedId);
			if (int.TryParse(decodedString, out int id))
			{
				var url = await this._repo.GetUrl(id);
				return url?.Url;
			}
			return null;
		}
	}
}

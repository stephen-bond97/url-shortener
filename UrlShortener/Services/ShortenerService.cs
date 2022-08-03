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
			// creating an instance of the shorturl repository
			this._repo = repo;
		}

		public async Task<string?> InsertShortUrl(string url)
		{
			// try to create a typed url object from the string
			// if result from try create is false, the string isn't a valid url
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
			// creating instance of base62 converter
			var converter = new Base62Converter();

			// decoding the string and outputting an Id to retrieve the Url
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

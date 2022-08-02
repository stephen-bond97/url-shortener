using UrlShortener.Models;

namespace UrlShortener.Data
{
	public interface IShortUrlRepository
	{
		Task<ShortUrl?> GetUrl(int id);
		Task<string> InsertShortUrl(string url);
	}
}
using UrlShortener.Models;

namespace UrlShortener.Data
{
	public interface IShortUrlRepository
	{
		Task<ShortUrl?> GetUrl(int id);
		Task<long> InsertShortUrl(string url);
	}
}
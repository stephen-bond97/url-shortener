namespace UrlShortener.Services
{
	public interface IShortenerService
	{
		Task<string?> GetShortUrl(string encodedId);
		Task<string?> InsertShortUrl(string url);
	}
}
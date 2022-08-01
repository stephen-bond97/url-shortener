namespace UrlShortener.Services
{
	public interface IShortenerService
	{
		Task<string> GetShortUrl(string url);
	}
}
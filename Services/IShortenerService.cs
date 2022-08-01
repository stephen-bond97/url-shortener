namespace url_shortener.Services
{
	public interface IShortenerService
	{
		Task<string> GetShortUrl(string url);
	}
}
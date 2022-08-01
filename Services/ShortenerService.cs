namespace url_shortener.Services
{
	public class ShortenerService: IShortenerService
	{
		public Task<string> GetShortUrl(string url)
		{
			return Task.FromResult("Hello World " + url);
		}
	}
}

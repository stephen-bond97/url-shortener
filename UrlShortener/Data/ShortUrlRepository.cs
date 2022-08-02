using UrlShortener.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Data
{
	public class ShortUrlRepository : IShortUrlRepository
	{
		private ShortenerContext _context;

		public ShortUrlRepository()
		{
			this._context = new ShortenerContext();
		}

		public async Task<string> InsertShortUrl(string url)
		{
			var newUrl = new ShortUrl();
			newUrl.Url = url;
			this._context.ShortUrls.Add(newUrl);
			await this._context.SaveChangesAsync();

			return newUrl.Id.ToString();
		}

		public async Task<ShortUrl?> GetUrl(int id)
		{
			return await this._context.ShortUrls.FirstOrDefaultAsync(u => u.Id == id);
		}
	}
}

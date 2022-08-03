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
			// creating an instance of the EF database context
			this._context = new ShortenerContext();
		}

		public async Task<long> InsertShortUrl(string url)
		{
			// creating an instance of the ShortUrl object to write to the database
			var newUrl = new ShortUrl();
			newUrl.Url = url;
			this._context.ShortUrls.Add(newUrl);

			// saving database state
			await this._context.SaveChangesAsync();

			// returning the new Id
			return newUrl.Id;
		}

		public async Task<ShortUrl?> GetUrl(int id)
		{
			// finding the url by Id
			return await this._context.ShortUrls.FirstOrDefaultAsync(u => u.Id == id);
		}
	}
}

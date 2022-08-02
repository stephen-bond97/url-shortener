using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ShortenerController : Controller
	{
		private readonly ILogger<ShortenerController> _logger;
		private readonly IShortenerService _shortenerService;

		public ShortenerController(
			ILogger<ShortenerController> logger, 
			IShortenerService shortenerService)
		{
			this._logger = logger;
			this._shortenerService = shortenerService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string id)
		{
			var getUrl = await this._shortenerService.GetShortUrl(id);
			if (getUrl == null)
			{
				return NotFound();
			}
			return Ok(getUrl);
		}

		[HttpPost]
		public async Task<IActionResult> CreateShortUrl(string url)
		{
			var shortUrl = await this._shortenerService.InsertShortUrl(url);
			return Ok(shortUrl);
		}
	}
}

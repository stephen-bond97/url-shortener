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

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var originalUrl = await this._shortenerService.GetShortUrl(id);
			if (originalUrl == null)
			{
				return NotFound();
			}
			return Redirect(originalUrl);
		}

		[HttpPost]
		public async Task<IActionResult> CreateShortUrl(string url)
		{
			if (string.IsNullOrWhiteSpace(url))
			{
				return BadRequest();
			}

			var shortUrl = await this._shortenerService.InsertShortUrl(url);
			if (shortUrl == null)
			{
				return BadRequest();
			}

			return Ok(shortUrl);
		}
	}
}

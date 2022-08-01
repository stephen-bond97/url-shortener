using Microsoft.AspNetCore.Mvc;
using url_shortener.Services;

namespace url_shortener.Controllers
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
		public async Task<IActionResult> Get(string url)
		{
			var shortUrl = await this._shortenerService.GetShortUrl(url);
			return Ok(shortUrl);
		}
	}
}

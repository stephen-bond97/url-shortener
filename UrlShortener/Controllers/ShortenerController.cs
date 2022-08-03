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
			// getting original url from user input and retrieve original url from database
			var originalUrl = await this._shortenerService.GetShortUrl(id);
			if (originalUrl == null)
			{
				// return not found http response if no original url is found in database
				return NotFound();
			}
			// return a redirect response to the original link
			return Redirect(originalUrl);
		}

		[HttpPost]
		public async Task<IActionResult> CreateShortUrl(string url)
		{
			// if no url is found in url parameters, this is a bad request
			if (string.IsNullOrWhiteSpace(url))
			{
				return BadRequest();
			}

			// adding original url to database and getting Id back
			var shortUrl = await this._shortenerService.InsertShortUrl(url);

			// if the shortUrl Id is null, return a bad request
			if (shortUrl == null)
			{
				return BadRequest();
			}

			// return an Ok http response if shortUrl is found
			return Ok(shortUrl);
		}
	}
}

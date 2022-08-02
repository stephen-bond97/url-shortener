using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Test
{
	public class ShortenerServiceTest
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task InsertShortUrl_ValidUrl_ReturnsBase62Id()
		{
			// arrange
			var validUrl = "https://google.com";
			var repoMock = new Mock<IShortUrlRepository>();
			repoMock
				.Setup(x => x.InsertShortUrl(validUrl))
				.ReturnsAsync(100);
			
			var service = new ShortenerService(repoMock.Object);

			// act
			var result = await service.InsertShortUrl(validUrl);

			// assert
			Assert.IsTrue(result == "DWbY");
		}

		[Test]
		public async Task InsertShortUrl_InvalidUrl_ReturnsNull()
		{
			// arrange
			var repoMock = new Mock<IShortUrlRepository>();
			var service = new ShortenerService(repoMock.Object);

			// act
			var result = await service.InsertShortUrl("invalid");

			// assert
			Assert.IsTrue(result == null);
		}

		[TestCase("***")]
		[TestCase("{}{}")]
		[TestCase(" ")]
		[TestCase("")]
		public async Task GetShortUrl_InvalidBase62String_ReturnsNull(string encodedId)
		{
			// arrange
			var repoMock = new Mock<IShortUrlRepository>();
			var service = new ShortenerService(repoMock.Object);

			// act
			var result = await service.GetShortUrl(encodedId);

			// assert
			Assert.IsTrue(result == null);
		}

		[Test]
		public async Task GetShortUrl_ValidBase62String_ReturnsUrl()
		{
			// arrange
			var shortUrl = new ShortUrl();
			shortUrl.Id = 1;
			shortUrl.Url = "https://google.com";
			var repoMock = new Mock<IShortUrlRepository>();
			repoMock
				.Setup(x => x.GetUrl(1))
				.ReturnsAsync(shortUrl);
			var service = new ShortenerService(repoMock.Object);

			// act
			var result = await service.GetShortUrl("n");

			// assert
			Assert.IsTrue(result == "https://google.com");
		}
	}
}
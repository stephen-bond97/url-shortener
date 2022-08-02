using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UrlShortener.Data;
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
			var repoMock = new Mock<IShortUrlRepository>();
			repoMock
				.Setup(x => x.InsertShortUrl(It.IsAny<string>()))
				.ReturnsAsync(100);
			var service = new ShortenerService(repoMock.Object);

			// act
			var result = await service.InsertShortUrl("https://google.com");

			// assert
			Assert.IsTrue(result == "DWbY");
		}
	}
}
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
	public class ShortUrl
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public string Url { get; set; } = string.Empty;
	}
}

using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Models
{
	public partial class ShortenerContext : DbContext
	{
		public ShortenerContext()
		{

		}

		public ShortenerContext(DbContextOptions<ShortenerContext> options)
		   : base(options)
		{
		}

		public virtual DbSet<ShortUrl> ShortUrls { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var connectionString = Environment.GetEnvironmentVariable("pgsql_connection_string");

				//optionsBuilder.UseNpgsql("Host=localhost;Database=entitycore;Username=postgres;Password=su_root");
				optionsBuilder.UseNpgsql(connectionString);

			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ShortUrl>(entity =>
			{
				entity.ToTable("shorturl");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.UseIdentityAlwaysColumn();

				entity.Property(e => e.Url).HasColumnName("url");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}

}

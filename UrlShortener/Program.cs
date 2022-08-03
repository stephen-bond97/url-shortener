using UrlShortener.Data;
using UrlShortener.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var modelBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

modelBuilder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://stephen-bond97.github.io",
                                             "http://localhost:4200/");
                      });
});

modelBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
modelBuilder.Services.AddEndpointsApiExplorer();
modelBuilder.Services.AddSwaggerGen();
modelBuilder.Services.AddScoped<IShortenerService, ShortenerService>();
modelBuilder.Services.AddScoped<IShortUrlRepository, ShortUrlRepository>();

var app = modelBuilder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();

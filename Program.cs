using UrlShortener.Services;

var modelBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

modelBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
modelBuilder.Services.AddEndpointsApiExplorer();
modelBuilder.Services.AddSwaggerGen();
modelBuilder.Services.AddScoped<IShortenerService, ShortenerService>();

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

app.Run();

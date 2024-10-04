using Groko.Core.Repositories;
using Groko.Repository;
using Groko.core.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on port 5000 on all IP addresses
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Listen on port 5000
});

// Add services to the container
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IBlogBytesService, BlogBytesService>();
builder.Services.AddScoped<IBlogBytesRepository, BlogBytesRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration for MySQL Connection String from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsBuilder => corsBuilder.WithOrigins("http://localhost:3000")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

// Set up API routes
app.MapControllers();

app.Run();

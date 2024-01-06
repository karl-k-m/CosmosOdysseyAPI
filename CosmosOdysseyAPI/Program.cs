using CosmosOdysseyAPI.BackgroundProcesses;
using CosmosOdysseyAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<UpdateFlightsBackgroundService>();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("CosmosOdysseyDB"));

// Add controllers here
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Set up swagger
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CosmosOdysseyAPI v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
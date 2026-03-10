using Backend.Extensions;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<HackerNewsService>();
builder.AddHackerNewsClient(); // this shows how you could use extension method for registering client

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAngularDev",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAngularDev");

app.MapControllers();

app.Run();

public partial class Program { }

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<MoviesAPIContext>(options =>
{
    options.UseInMemoryDatabase("Movies");
    //options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesAPIContext") ?? throw new InvalidOperationException("Connection string 'MoviesAPIContext' not found."));

});

builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5005";
                    options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
                });

// Add claim based authorization policy

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id","movieClient"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed DB
using var scope = app.Services.CreateScope();
var moviesContext = scope.ServiceProvider.GetRequiredService<MoviesAPIContext>();
MoviesContextSeed.SeedAsync(moviesContext);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
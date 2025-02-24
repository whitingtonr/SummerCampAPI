using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SummerCampAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
  policy =>
  {
    policy.AllowAnyOrigin(); //set the allowed origin
    // policy.AllowAnyMethod();
  });
});

var conString = builder.Configuration.GetConnectionString("DevDb") ??
     throw new InvalidOperationException("Connection string 'DevDb'" +
    " not found.");

builder.Services.AddControllers();
builder.Services.AddDbContext<RegistrationsContext>(options => options.UseSqlServer(conString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
  // app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

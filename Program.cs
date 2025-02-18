using Microsoft.EntityFrameworkCore;
using SummerCampAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("DevDb") ??
     throw new InvalidOperationException("Connection string 'DevDb'" +
    " not found.");

builder.Services.AddControllers();
builder.Services.AddDbContext<RegistrationsContext>(options => options.UseSqlServer(conString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

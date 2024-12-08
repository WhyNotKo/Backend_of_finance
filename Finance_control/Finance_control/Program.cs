using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Finance_control.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Finance_controlContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Finance_controlContext") ?? throw new InvalidOperationException("Connection string 'Finance_controlContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

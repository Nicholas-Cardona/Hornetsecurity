using Hornet.Api.Service;
using Hornet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string? cs = builder.Configuration.GetConnectionString("MySql") ?? throw new Exception("No Connection string");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(cs, b => b.MigrationsAssembly("Hornet.Data"));
}
);

builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

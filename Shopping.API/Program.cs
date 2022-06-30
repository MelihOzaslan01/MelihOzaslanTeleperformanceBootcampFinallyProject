using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.API.Extensions;
using Shopping.Application;
using Shopping.Domain.Entities;
using Shopping.Infrastructure;
using Shopping.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("Default"))
);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();

var app = builder.Build();
app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

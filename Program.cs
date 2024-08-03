using Microsoft.AspNetCore.Identity;
using WebAppApi.Entities;
using WebAppApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// extensions for services
builder.Services.AddApplicationServices(configuration);
builder.Services.AddIdentityServices(configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
RoleManager<Role> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
string[] roleNames = ["User", "Admin"];
foreach (string roleName in roleNames)
{
    bool roleExist = await roleManager.RoleExistsAsync(roleName);
    if (!roleExist)
    {
        await roleManager.CreateAsync(new Role { Name = roleName });
    }
}

// use CORS policy that before i've instanced
app.UseCors(options => options.AllowAnyMethod().AllowCredentials().AllowAnyHeader().WithOrigins("http://localhost:4200", "https://localhost:4200"));

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

using Microsoft.EntityFrameworkCore;
using MyApp.Core.Data;
using MyApp.Core.Repository.Users;
using MyApp.Core.Builder;
using My.Core.Repository.Base;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000") // adjust to your React dev server
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserBuilder, UserBuilder>();



var app = builder.Build();


// Middleware setup
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

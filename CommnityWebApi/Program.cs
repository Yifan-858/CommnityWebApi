using CommnityWebApi.Data;
using CommnityWebApi.Data.Interfaces;
using CommnityWebApi.Data.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration
                    .GetConnectionString("DefaultConnection");
//DI
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();

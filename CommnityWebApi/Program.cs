using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Core.Services;
using CommnityWebApi.Data;
using CommnityWebApi.Data.Interfaces;
using CommnityWebApi.Data.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration
                    .GetConnectionString("DefaultConnection");
var jwtConfig = builder.Configuration.GetSection("Jwt");

builder.Services.AddControllers();

//DI
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(opt => {
       opt.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = jwtConfig["Issuer"],
           ValidAudience = jwtConfig["Audience"],
           IssuerSigningKey =
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]))
       };
   });

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();

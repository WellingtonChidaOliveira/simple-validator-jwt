using System.Text;
using AuthJwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("User", policy => policy.RequireRole("user"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/", () => "Hello World!");

app.MapPost("/login", (User user) =>
{
    var getUser = UserRepository.Get(user.Name, user.Password);
    if (getUser == null)
        return Results.Unauthorized();
    
    var token = TokenService.GenerateToken(getUser);
    user.Password = string.Empty;
    return Results.Ok(new {getUser,token});
});

app.MapGet("/anonymous", () => "Hello anonymous");

app.MapGet("/authenticated", () => "Hello authenticated").RequireAuthorization();

app.MapGet("/admin", () => "Hello Admin").RequireAuthorization("Admin");

app.MapGet("/user", ()=> "Hello user").RequireAuthorization("User");



app.Run();

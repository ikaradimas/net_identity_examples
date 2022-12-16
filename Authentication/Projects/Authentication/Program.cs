using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

const string AuthScheme = "cookie";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(AuthScheme).AddCookie(AuthScheme);
builder.Services.AddAuthorization(builder => 
{
    builder.AddPolicy("eu passport", pb => 
    {
        pb.RequireAuthenticatedUser()
            .AddAuthenticationSchemes(AuthScheme)
            .RequireClaim("passport", "eu");
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/unsecure", (HttpContext ctx) => 
{
    return ctx.User.FindFirst("usr")?.Value ?? "empty";
}).RequireAuthorization("eu passport");

app.MapGet("/login", async (HttpContext ctx) => 
{
    var claims = new List<Claim>();
    claims.Add(new ("usr", "john"));
    claims.Add(new ("passport", "eu"));
    var identity = new ClaimsIdentity(claims, "cookie");
    var user = new ClaimsPrincipal(identity);

    await ctx.SignInAsync("cookie", user);

    return "ok";
}).AllowAnonymous();

app.Run();
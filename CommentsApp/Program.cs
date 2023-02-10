using CommentsApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<UserContext>()
            .AddDefaultTokenProviders();
builder.Services.AddDbContext<UserContext>(options =>
            options.UseSqlServer("Server=.\\SQLExpress;Database=CommentsAppDatabase;Trusted_Connection=true;TrustServerCertificate=true"));

builder.Services.AddDbContext<CommentsContext>(options =>
            options.UseSqlServer("Server=.\\SQLExpress;Database=CommentsAppDatabase;Trusted_Connection=true;TrustServerCertificate=true"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}
).AddCookie(options =>
{
    options.SlidingExpiration = true;
})
    .AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/signin-google",async context =>
//{
//    var authenticationProperties = new AuthenticationProperties
//    {
//        RedirectUri = "/home",
//        Items = { { "scheme", GoogleDefaults.AuthenticationScheme } }
//    };
//    await context.ChallengeAsync(authenticationProperties);

//});



app.Run();



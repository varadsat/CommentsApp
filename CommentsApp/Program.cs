using CommentsApp.Data;
using CommentsApp.DTOs;
using CommentsApp.Services.AssetServices;
using CommentsApp.Services.CommentServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json;
using System.Text.Json.Serialization;

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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAssetService, AssetService>();

JsonSerializerOptions options = new()
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
    WriteIndented = true
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/signin-google", async context =>
//{
//    var authenticationProperties = new AuthenticationProperties
//    {
//        RedirectUri = "/home",
//        Items = { { "scheme", GoogleDefaults.AuthenticationScheme } }
//    };
//    await context.ChallengeAsync(authenticationProperties);

//});

app.MapPost("/add-comment", async (CommentRequestDto comment, ICommentService commentService) =>
{
    var commentCreated = await commentService.CreateComment(comment);
    string response = JsonSerializer.Serialize(commentCreated, options);
    return commentCreated;
});
app.MapPut("/add-child-comment", async (CommentRequestDto comment, int parentId, ICommentService commentService) =>
{
    return await commentService.CreateChildComment(comment, parentId);
});
app.MapGet("/get-comments", async (ICommentService commentService) =>
{
    return await commentService.GetAllComments();
});
app.MapGet("/get-child-comments", async (int parentId, ICommentService commentService) =>
{
    return await commentService.GetChildComments(parentId);
});
app.MapDelete("del-comment", async (int id, ICommentService commentService) =>
{
    await commentService.DeleteComment(id);
    return Results.Ok();
});

app.Run();



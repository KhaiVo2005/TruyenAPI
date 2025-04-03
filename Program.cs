using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TruyenAPI.Data;
using TruyenAPI.Models;
using TruyenAPI.Repositories.ChapterImageRes;
using TruyenAPI.Repositories.ChapterRes;
using TruyenAPI.Repositories.StoryRes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TruyenDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("TruyenDb")));
builder.Services.AddDbContext<TruyenAuthDbContext>(opption => opption.UseSqlServer(builder.Configuration.GetConnectionString("TruyenAuthDB")));

builder.Services.AddScoped<IStoryRepository, StoryRespository>();
builder.Services.AddScoped<IChapterRespository, ChapterRespository>();
builder.Services.AddScoped<IChapterImages, ChapterImageRespository>();

builder.Services.AddIdentityCore<IdentityUser>(option =>
{
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequiredUniqueChars = 1;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireDigit = false;
    option.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("TruyenAPI")
    .AddEntityFrameworkStores<TruyenAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    option => option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
    });

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

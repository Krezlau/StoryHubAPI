using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StoryHubAPI.Data;
using StoryHubAPI.Models;
using StoryHubAPI.Repository;
using StoryHubAPI.Repository.IRepository;
using StoryHubAPI.Services;
using System.Security.Principal;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db context
builder.Services.AddDbContext<StoryHubDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("extremelySecretKey")), // for now
        ValidateIssuer = false, // for dev
        ValidateAudience = false, // for dev
        ValidateLifetime = true
    };
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<StoryHubDbContext>()
    .AddTokenProvider("StoryHub", typeof(DataProtectorTokenProvider<User>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStoryRepository, StoryRepository>();
builder.Services.AddScoped<IRepository<Like>, Repository<Like>>();
builder.Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
builder.Services.AddScoped<IRepository<Tag>, Repository<Tag>>();
builder.Services.AddScoped<IRepository<User>, Repository<User>>();

builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "StoryHubAPI");
    options.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

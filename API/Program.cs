using System.Text;
using API.Configurations;
using API.Model;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.Configure<OpenAiConfig>(config =>
{
  config.ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new ArgumentNullException("OPENAI_API_KEY");
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "CorsPolicy", policy =>
  {
    policy.WithOrigins("https://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // allow credentials only works with specific origin for cookie based auth like google auth or jwt auth with cookies methods
  });
});

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(
  options =>
  {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
  }

)
.AddGoogle(
  options =>
  {
    options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_OAUTH_CLIENT_ID");
    options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_OAUTH_CLIENT_SECRET");
    options.CallbackPath = "/signin-google";

  }
)

.AddFacebook(
  options =>
  {
    options.AppId = Environment.GetEnvironmentVariable("FACEBOOK_APP_ID") ?? throw new ArgumentNullException("FACEBOOK_APP_ID");
    options.AppSecret = Environment.GetEnvironmentVariable("FACEBOOK_APP_SECRET") ?? throw new ArgumentNullException("FACEBOOK_APP_SECRET");
  })

.AddJwtBearer(options =>
{
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidateAudience = true,
    ValidAudience = builder.Configuration["Jwt:Audience"],
    ValidateLifetime = true,
    IssuerSigningKey = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
    ),
    ValidateIssuerSigningKey = true,
  };
});



builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
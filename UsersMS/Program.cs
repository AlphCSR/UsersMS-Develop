using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using UsersMS.Application.Handlers.Commands;
using UsersMS.Application.Handlers.Querys;
using UsersMS.Core.DataBase;
using UsersMS.Core.Repositories;
using UsersMS.Core.Service;
using UsersMS.Infrastructure.DataBase;
using UsersMS.Infrastructure.Repositories;
using UsersMS.Infrastructure.Service;
using UsersMS.Infrastructure.Setings;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var _appSettings = new AppSettings();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
_appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddHttpClient();

builder.Services.AddMediatR(typeof(CreateAuctioneerCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetTechnicalSupportQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteAuctioneerCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateAuctioneerCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllBiddersQueryHandler).Assembly);

builder.Services.AddMediatR(typeof(CreateTechnicalSupportCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAuctioneersQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteTechnicalSupportCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateTechnicalSupportCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllTechnicalSupportsQueryHandler).Assembly);

builder.Services.AddMediatR(typeof(CreateBidderCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteBidderCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateBidderCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetBidderQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllAuctioneersQueryHandler).Assembly);

builder.Services.AddMediatR(typeof(CreateAdministratorCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteAdministratorCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAdministratorQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllAdministratorsQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateAdministratorCommandHandler).Assembly);

builder.Services.AddTransient<IUsersDbContext, UsersDbContext>();
builder.Services.AddTransient<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddScoped<IKeycloakService, KeycloakService>();
builder.Services.AddTransient<IBidderRepository, BidderRepository>();
builder.Services.AddTransient<ITechnicalSupportRepository, TechnicalSupportRepository>();
builder.Services.AddTransient<IAuctioneerRepository, AuctioneerRepository>();

builder.Services.AddTransient<IEmailService, EmailService>();

var dbConnectionString = builder.Configuration.GetValue<string>("DefaultConnection");
builder.Services.AddDbContext<UsersDbContext>(options =>
options.UseSqlServer(dbConnectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Introduce el token JWT en el formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8080/realms/Users-Ms";
    options.Audience = "publi-client";
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:8080/realms/Users-Ms",
        ValidateAudience = true,
        ValidAudience = "publi-client",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var resourceAccess = context.Principal.FindFirst("resource_access")?.Value;
                if (!string.IsNullOrEmpty(resourceAccess))
                {
                    var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                    if (resourceAccessJson.RootElement.TryGetProperty("publi-client", out var publiClientElement) &&
                        publiClientElement.TryGetProperty("roles", out var rolesElement))
                    {
                        foreach (var role in rolesElement.EnumerateArray())
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()));
                        }
                    }
                }
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();  
app.MapControllers();
app.Run();


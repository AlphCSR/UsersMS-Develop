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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Algo parecido a las variables goblales en el typescript 
var _appSettings = new AppSettings();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
_appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<AppSettings>(appSettingsSection);

//Servicios para instancear los handlers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Ignorar mayúsculas y minúsculas
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()); // Convertir enums automáticamente
    });


builder.Services.AddHttpClient(); // Registrar HttpClient para manejar solicitudes HTTP


//Handlers Conductores
builder.Services.AddMediatR(typeof(CreateAuctioneerCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetTechnicalSupportQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteAuctioneerCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateAuctioneerCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllBiddersQueryHandler).Assembly);

//Handlers Proveedores
builder.Services.AddMediatR(typeof(CreateTechnicalSupportCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAuctioneersQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteTechnicalSupportCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateTechnicalSupportCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllTechnicalSupportsQueryHandler).Assembly);

//Handlers Operadores
builder.Services.AddMediatR(typeof(CreateBidderCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteBidderCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateBidderCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetBidderQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllAuctioneersQueryHandler).Assembly);

//Handlers Administradores
builder.Services.AddMediatR(typeof(CreateAdministratorCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteAdministratorCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAdministratorQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllAdministratorsQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateAdministratorCommandHandler).Assembly);



//Registrando los servicios de dbContext y repo para los usuarios
builder.Services.AddTransient<IUsersDbContext, UsersDbContext>();
builder.Services.AddTransient<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddScoped<IKeycloakService, KeycloakService>();
builder.Services.AddTransient<IBidderRepository, BidderRepository>();
builder.Services.AddTransient<ITechnicalSupportRepository, TechnicalSupportRepository>();
builder.Services.AddTransient<IAuctioneerRepository, AuctioneerRepository>();

//Registrandor servicios para enviar Email
builder.Services.AddTransient<IEmailService, EmailService>();


var dbConnectionString = builder.Configuration.GetValue<string>("DefaultConnection");
builder.Services.AddDbContext<UsersDbContext>(options =>
options.UseSqlServer(dbConnectionString));

// Configure Swagger to allow JWT token input
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


// JWT Authentication configuration
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
            // Obtener el token validado
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                // Extraer los roles desde "resource_access.publi-client.roles"
                var resourceAccess = context.Principal.FindFirst("resource_access")?.Value;

                if (!string.IsNullOrEmpty(resourceAccess))
                {
                    var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                    if (resourceAccessJson.RootElement.TryGetProperty("publi-client", out var publiClientElement) &&
                        publiClientElement.TryGetProperty("roles", out var rolesElement))
                    {
                        foreach (var role in rolesElement.EnumerateArray())
                        {
                            // Agregar cada rol como un claim estándar
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable authentication and authorization middleware
app.UseAuthentication(); // This middleware will authenticate incoming requests
app.UseAuthorization();  // This middleware will authorize incoming requests

app.MapControllers();

app.Run();


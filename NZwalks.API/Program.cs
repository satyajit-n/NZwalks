using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Mappings;
using NZwalks.API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;
using System.Net.NetworkInformation;
using Microsoft.Extensions.FileProviders;
using Serilog;
using NZwalks.API.Middlewares;
using Microsoft.Extensions.Options;

internal class Program
{
    private static void Main(string[] args)
    {
        //creating instance of class WebApplication allows you to configure various aspects of the web application,
        //such as services, middleware, and routing, before building and running the application.
        var builder = WebApplication.CreateBuilder(args);


        //Injecting Logger to the container
        var Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/NZWalks_Logs.txt",rollingInterval: RollingInterval.Day)
            .MinimumLevel.Warning()
            .CreateLogger();

        //Clearing any provider that were injected till now
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Logger);

        //adds the controllers-related services to the dependency injection container.
        //this enable ASP.NET Core to handle incoming requests, route them to the appropriate controllers, and execute the corresponding action methods.
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //AddEndpointsApiExplorer is used to register the necessary services for API exploration and documentation generation,
        //allowing you to work with and generate documentation for your API endpoints
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "NZ Walks API",
                Version = "v1"
            });
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
               Name = "Authorization",
               In = ParameterLocation.Header,
               Type = SecuritySchemeType.ApiKey,
               Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    },
                        Scheme = "Oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        //NZWalksDbContext is registered as a service in the ASP.NET Core application's service collection,
        //using the SQL Server provider and a connection string retrieved from the configuration,
        //this allows the application to access and interact with the database using the NZWalksDbContext database context.
        builder.Services.AddDbContext<NZWalksDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

        builder.Services.AddDbContext<NZwalksAuthDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")));

        builder.Services.AddDbContext<CrudWithPostgres>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("CRUDOperations")));

        //This line injects IWalkRepository with implementation SQLWalkRepository
        builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();

        //This line injects IRegionRepository with implementation SQLRegionRepository
        builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

        builder.Services.AddScoped<ITokenRepository, SQLTokenRopository>();
        builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

        //Injecting Automapper when program starts
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

        //This are the steps are to set up identity along with jwt authentication
        builder.Services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>() // Adding roles
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
            .AddEntityFrameworkStores<NZwalksAuthDbContext>() // Identity will use this store
            .AddDefaultTokenProviders(); // used generate token for change emails, reset passwords, 


        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        //Adding Authentication to the services
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlerMiddleware>();

        // adds a middleware to the HTTP request pipeline that automatically redirects HTTP requests to HTTPS.
        // provides additional layer of security.
        app.UseHttpsRedirection();


        app.UseAuthentication();

        //adds the authorization middleware to the HTTP request pipeline.
        app.UseAuthorization();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images")),
            RequestPath = "/Images"

        });

        app.MapControllers();

        app.Run();
    }
}
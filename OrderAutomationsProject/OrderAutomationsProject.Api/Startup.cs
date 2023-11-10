using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderAutomationsProject.Data.Context;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Mapper;
using OrderAutomationsProject.Base.Token;
using System.Data;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OrderAutomationsProject.Api.Middlewares;
using OrderAutomationsProject.Data.Helpers;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using OrderAutomationsProject.Operation.Hubs;

namespace OrderAutomationsProject.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        string connection = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<OaDbContext>(options => options.UseSqlServer(connection));

        services.AddScoped<IDbConnection>(x => new SqlConnection(Configuration.GetConnectionString("MsSqlConnection")));

        var JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(typeof(CreateAddressCommand).GetTypeInfo().Assembly);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        services.AddSingleton(config.CreateMapper());

        services.AddHttpContextAccessor();

        services.AddSingleton<ISessionService, SessionService>();

        services.AddAuthorization();

        services.AddSignalR();

        services.AddSingleton<ILoggerService, DBLogger>();
        services.AddSingleton<ILoggerService, ConsoleLogger>();

        services.AddControllers();

        services.AddMvc();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "VkApi Api Management", Version = "v1.0" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "OrderAutomation Management",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
        });

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
                ValidAudience = JwtConfig.Audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });

        services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowAllHeaders",
            builder =>
            {
                builder.AllowAnyHeader()
                       .WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderAutomationApi v1"));
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // auth
        app.UseAuthentication();
        app.UseRouting();

        app.UseCors("AllowAllHeaders");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<MessageHub>("/messageHub");
        });

        app.UseCustomExceptionMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

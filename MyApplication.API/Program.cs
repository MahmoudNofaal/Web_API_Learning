using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApplication.API.Data;
using MyApplication.API.Mappings;
using MyApplication.API.Repositories;
using MyApplication.API.Repositories.Interfaces;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

namespace MyApplication.API;

public class Program
{
   public static void Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Logging.ClearProviders();
      builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.Console()
                                                          .MinimumLevel.Information()
                                                          .CreateLogger());

      builder.Services.AddControllers();
      builder.Services.AddHttpContextAccessor();

      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      builder.Services.AddOpenApi();

      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(options =>
      {
         options.SwaggerDoc("v1", new OpenApiInfo()
         {
            Title = "MyApplication API",
            Version = "v1",
         });

         options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
         {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
         });

         options.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
            {
               new OpenApiSecurityScheme
               {
                  Reference = new OpenApiReference
                  {
                     Type = ReferenceType.SecurityScheme,
                     Id = JwtBearerDefaults.AuthenticationScheme,
                  },
                  Scheme = "Oauth2",
                  Name = JwtBearerDefaults.AuthenticationScheme,
                  In = ParameterLocation.Header,
               },
               new List<string>()
            },
         });
      });


      builder.Services.AddDbContext<ApplicationDbContext>(options =>
      {
         options.UseSqlServer(builder.Configuration.GetConnectionString("MyApplicateDB"));
      });

      builder.Services.AddDbContext<ApplicationAuthDbContext>(options =>
      {
         options.UseSqlServer(builder.Configuration.GetConnectionString("MyApplicationAuthDB"));
      });

      //builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();
      builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
      builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();

      builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


      builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(options =>
                      {
                         options.TokenValidationParameters = new TokenValidationParameters()
                         {
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                         };
                      });

      builder.Services.AddIdentityCore<IdentityUser>()
                      .AddRoles<IdentityRole>()
                      .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("MyApplication")
                      .AddEntityFrameworkStores<ApplicationAuthDbContext>()
                      .AddDefaultTokenProviders();

      builder.Services.Configure<IdentityOptions>(options =>
      {
         options.Password.RequireDigit = false;
         options.Password.RequireLowercase = false;
         options.Password.RequireUppercase = false;
         options.Password.RequireNonAlphanumeric = false;
         options.Password.RequiredLength = 8;
         options.Password.RequiredUniqueChars = 1;
      });

      builder.Services.AddScoped<ITokenRepository, TokenRepository>();

      builder.Services.AddScoped<IImageRepository, LocalImageRepository>();



      var app = builder.Build();

      // Configure the HTTP request pipeline.
      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
         app.UseSwagger();
         app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseStaticFiles();

      app.MapControllers();

      app.Run();

   }

}


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using FastCommerce.Business.CategoryManager.Abstract;
using FastCommerce.Business.CategoryManager.Concrete;
using FastCommerce.Business.ElasticSearch.Abstract;
using FastCommerce.Business.ElasticSearch.Concrete;
using FastCommerce.Business.OrderManager.Abstract;
using FastCommerce.Business.OrderManager.Concrete;
using FastCommerce.Business.ProductManager;
using FastCommerce.Business.ProductManager.Abstract;
using FastCommerce.Business.ProductManager.Concrete;
using FastCommerce.Business.UserManager;
using FastCommerce.Business.UserManager.Abstract;
using FastCommerce.Business.UserManager.Concrete;
using FastCommerce.DAL;
using FastCommerce.Entities.Models;
using FastCommerce.Web.API.Infrastructure;
using FastCommerce.Web.API.Interfaces;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Utility.MailServices;
using Utility.Models;

namespace FastCommerce.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
       
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDomainDataServices();
            services.AddScoped<IElasticSearchService, ElasticSearchManager>();
            services.AddScoped<IElasticSearchConfigration, ElasticSearchConfigration>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<IPropertyManager, PropertyManager>();
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddMemoryCache();
            services.AddCors();


            services.AddEmailSender(Configuration);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable("REDIS_IP");
            });

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FastCommerce API",
                    Description = "FastCommerce Web API",
                    TermsOfService = new Uri("https://github.com/mehmetutkuk/FastCommerce/wiki"),
                    Contact = new OpenApiContact
                    {
                        Name = "Mehmet Utku KUL",
                        Email = "m.utkukul@gmail.com",
                        Url = new Uri("https://github.com/mehmetutkuk"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache License 2.0",
                        Url = new Uri("https://github.com/mehmetutkuk/FastCommerce/blob/master/LICENSE"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer ASD123ASD123'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                  });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.Configure<TokenModel>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenModel>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                };
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IMapsterProfile, MapsterProfile>();
            var sp = services.BuildServiceProvider();
            var mapsterProfile = sp.GetService<IMapsterProfile>();
            mapsterProfile.Configure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FastCommerce API V1");

            });

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseHttpsRedirection();
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly());
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();

            //var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(localizeOptions.Value);
        }
    }

    public static class DomainDIExtensions
    {
        public static void AddDomainDataServices(this IServiceCollection services)
        {
            //string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            var connectionString = "host=postgres_image;port=5432;Database=fastCommerce;Username=postgres;Password=postgresPassword;";
            services.AddDbContext<dbContext>(options => options.UseNpgsql(connectionString, y => y.MigrationsAssembly("FastCommerce.DAL")));
            services.AddTransient<UserManager>();
            services.AddTransient<IProductManager,ProductManager>();
        }
    }
    public static class EmailExtensions
    {
        public static void AddEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            //    var config = configuration.GetSection("Email").Get<EmailConfig>();
            services.Configure<EmailConfig>(configuration.GetSection("Email"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }

}

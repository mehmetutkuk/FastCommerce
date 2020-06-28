using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nest;

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
            services.AddElasticsearch(Configuration);

            var ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<ProductContext>(options =>
            
            options.UseNpgsql(ConnectionString));

//            services.AddElasticsearch(Configuration);

            services.AddMemoryCache();
            
            services.AddStackExchangeRedisCache(options => { 
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
            });
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
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
       
    }
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<Product>(m => m
                    .PropertyName(p => p.ProductID, "ProductID")
                )
                .DefaultMappingFor<User>(m => m
                    .Ignore(c => c.ProfilePhoto)
                    .PropertyName(c => c.UserID, "UserID")
                );

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }

}

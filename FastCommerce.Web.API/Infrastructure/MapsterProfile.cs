using FastCommerce.Web.API.Interfaces;
using FastCommerce.Web.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Web.API.Infrastructure
{
    public class MapsterProfile : IMapsterProfile
    {
        private IWebHostEnvironment _env;
        public MapsterProfile(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void Configure()
        {
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);

            TypeAdapterConfig<Exception, ApiException>
            .NewConfig()
            .Map(dest => dest.Message, src => src.Message)
            .Map(dest => dest.Detail, src => src.StackTrace);
            
            

        }
    }
}
using CyrptoTrader.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeaturesGatherer.Services
{
    public static class ServiceMappings
    {
        public static IServiceCollection AddServiceMappings(this IServiceCollection services)
        {
            services.AddSingleton<IDataAccess, DataAccess>();
            services.AddSingleton<IIndicatorService, IndicatorService>();
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddSingleton<IFeatureWriterService, FeatureWriterService>();
            return services;
        }
    }
}

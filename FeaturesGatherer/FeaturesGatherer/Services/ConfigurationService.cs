using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FeaturesGatherer.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public IConfigurationRoot BuildAppSettingsConfig()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appSettings.json", false, true).Build();
            SetupConstants(configuration);
            return configuration;
        }

        private static void SetupConstants(IConfigurationRoot configuration)
        {
            Constants.OutputClasses = new OutputClasses();
            var outputClassesSection = configuration.GetSection("OutputClasses");
            outputClassesSection.Bind(Constants.OutputClasses);
            Constants.OutputDirectory = configuration.GetValue<string>("directory");
        }
    }
}

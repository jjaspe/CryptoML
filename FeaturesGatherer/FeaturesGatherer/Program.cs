using FeaturesGatherer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FeaturesGatherer
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddServiceMappings()
                .BuildServiceProvider();

            services.GetService<IConfigurationService>().BuildAppSettingsConfig();
            string directory = Constants.OutputDirectory;
            services.GetService<IFeatureWriterService>()
                .WriteTrainingData(directory+"features.txt",
                directory + "results.txt", directory + "verificationFeatures.txt",
                directory + "verificationResults.txt");
        }
    }
}

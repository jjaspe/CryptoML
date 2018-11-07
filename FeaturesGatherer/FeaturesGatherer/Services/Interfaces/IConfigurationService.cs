using Microsoft.Extensions.Configuration;

namespace FeaturesGatherer.Services
{
    public interface IConfigurationService
    {
        IConfigurationRoot BuildAppSettingsConfig();
    }
}
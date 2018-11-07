namespace FeaturesGatherer.Services
{
    public interface IFeatureWriterService
    {
        void WriteTrainingData(string featuresFilename, string resultsFilename ,string a,string b);
    }
}
using System.Threading.Tasks;

namespace WeatherTelemetry
{
    public interface ISensorReader
    {
        Task InitializeHat();
        void ReadSensors(out double temperature, out double humidity);
    }
}
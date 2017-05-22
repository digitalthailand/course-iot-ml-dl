using GHIElectronics.UWP.Shields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTelemetry
{
    public class FezHatSensorReader : ISensorReader
    {
        public FEZHAT FezHat;

        private bool isLightOn;

        public FezHatSensorReader()
        {
            isLightOn = false;
        }

        public async Task InitializeHat()
        {
            FezHat = await FEZHAT.CreateAsync();
            FezHat.D2.TurnOff();
            FezHat.D3.TurnOff();
        }

        public void ReadSensors(out double temperature, out double humidity)
        {
            isLightOn = !isLightOn;
            if (isLightOn)
            {
                FezHat.D3.Color = FEZHAT.Color.Green;
            }
            else
            {
                FezHat.D3.TurnOff();
            }

            temperature = FezHat.GetTemperature();
            humidity = 20 + FezHat.GetLightLevel() * 25;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTelemetry
{
    public class SensorReader
    {
        //public ISenseHat SenseHat;

        public SensorReader()
        {
            InitializeSenseHat();
        }

        public async Task InitializeSenseHat()
        {
            //ISenseHat senseHat = await SenseHatFactory.GetSenseHat();
            //this.SenseHat = senseHat;
        }
    }
}

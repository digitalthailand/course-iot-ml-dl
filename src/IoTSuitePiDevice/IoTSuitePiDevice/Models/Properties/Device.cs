using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSuitePiDevice.Models.Properties
{
    public class Device
    {
        public string DeviceState { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSuitePiDevice.Models
{
    public class DeviceProperties
    {
        public string DeviceID { get; set; }
        public bool? HubEnabledState { get; set; }
        public string FirmwareVersion { get; set; }
        public string BatteryLevel { get; set; }
    }
}

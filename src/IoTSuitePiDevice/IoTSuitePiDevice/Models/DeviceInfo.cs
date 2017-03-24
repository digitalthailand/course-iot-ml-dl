using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSuitePiDevice.Models
{
    public class DeviceInfo
    {
        [JsonConstructor]
        public DeviceInfo()
        {
            Telemetry = new List<Models.Telemetry>();
        }

        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string Version { get; set; }
        public DeviceProperties DeviceProperties { get; set; }
        public List<Telemetry> Telemetry { get; set; }
    }
}

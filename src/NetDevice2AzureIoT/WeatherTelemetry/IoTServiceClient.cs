using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace WeatherTelemetry
{
    public class IoTServiceClient
    {
        private static readonly string DeviceId = "{device Id}";
        private static readonly string iotHubUri = "{iot hub hostname}";
        private static readonly string deviceKey = "{device key}";
        //private readonly string DeviceConnectionString = $"HostName={iotHubUri};DeviceId={DeviceId};SharedAccessKey={deviceKey}";

        private DeviceClient deviceClient;

        public IoTServiceClient()
        {
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(DeviceId, deviceKey), TransportType.Mqtt);
            InitializeIoTServiceConnection();
        }

        private async void InitializeIoTServiceConnection()
        {
            var deviceInfo = new
            {
                ObjectType = "DeviceInfo",
                ObjectName = DeviceId,
                Version = "1.0",
                DeviceProperties = new
                {
                    DeviceID = DeviceId,
                    HubEnabledState = true,
                    Manufacturer = "Digital Thailand Club",
                    FirmwareVersion = "1.0",
                    DeviceState = "normal",
                },
                IsSimulatedDevice = false,
            };

            await deviceClient.OpenAsync();
            await deviceClient.SendEventAsync(ToMessage(deviceInfo));
        }

        public async void SendTelemetry(double temperature, double humidity)
        {
            var telemetryDataPoint = new
            {
                deviceId = DeviceId,
                temperature = temperature,
                humidity = humidity
            };
            await deviceClient.SendEventAsync(ToMessage(telemetryDataPoint));
        }

        private Message ToMessage(object data)
        {
            var jsonText = JsonConvert.SerializeObject(data);
            var dataBuffer = System.Text.UTF8Encoding.UTF8.GetBytes(jsonText);
            return new Message(dataBuffer);
        }
    }
}

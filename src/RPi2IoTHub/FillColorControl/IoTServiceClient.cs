using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillColorControl
{
    public class IoTServiceClient
    {
        private static readonly string DeviceConnectionString = "{your device's connection string}";

        private DeviceClient deviceClient;

        private Action<Windows.UI.Color> SetFillColor;

        public IoTServiceClient(Action<Windows.UI.Color> setFillColor)
        {
            deviceClient = DeviceClient.CreateFromConnectionString(DeviceConnectionString, TransportType.Mqtt);
            SetFillColor = setFillColor;
        }

        public async Task InitializeIoTServiceConnection()
        {
            await deviceClient.OpenAsync();

            await deviceClient.SetMethodHandlerAsync(
                "fill",
                HandleFill, null);
        }

        private Task<MethodResponse> HandleFill(MethodRequest req, object ctx)
        {
            var json = req.DataAsJson;
            var obj = JToken.Parse(json);
            var colorCode = Convert.ToString(obj["color"]);
            Windows.UI.Color color;
            switch (colorCode)
            {
                case "red":
                    color = Windows.UI.Colors.Red;
                    break;
                case "green":
                    color = Windows.UI.Colors.Green;
                    break;
                case "blue":
                    color = Windows.UI.Colors.Blue;
                    break;
                case "yellow":
                    color = Windows.UI.Colors.Yellow;
                    break;
                default:
                    color = Windows.UI.Colors.Orange;
                    break;
            }
            SetFillColor(color);
            return Task.FromResult(new MethodResponse(200));
        }

        public async void SendTelemetry(double temperature, double humidity)
        {
            var telemetryDataPoint = new
            {
                Temperature = temperature,
                Humidity = humidity
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

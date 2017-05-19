using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Models;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Models.Commands;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTSuitePiDevice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string DeviceId = "";
        private static readonly string DeviceConnectionString = $"HostName=<host name>;DeviceId={DeviceId};SharedAccessKey=<key>";
        private DeviceClient client;

        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var client = DeviceClient.CreateFromConnectionString(DeviceConnectionString, TransportType.Mqtt);
            this.client = client;

            OutputLabel.Text = DeviceConnectionString;

            var deviceInfo = new DeviceModel
            {
                ObjectType = "DeviceInfo",
                ObjectName = DeviceId,
                Version = "1.0",
                DeviceProperties = new DeviceProperties
                {
                    DeviceID = "test1",
                    HubEnabledState = true,
                    Manufacturer = "Digital Thailand Club",
                    FirmwareVersion = "2.0",
                    DeviceState = "normal",
                },
                IsSimulatedDevice = false,
            };
            deviceInfo.Commands.Add(new Command("MyCommand", DeliveryType.Message, "My Command Description"));
            deviceInfo.Telemetry.Add(new Telemetry("Temperature", "Temperature", "double"));
            deviceInfo.Telemetry.Add(new Telemetry("LightLevel", "LightLevel", "double"));

            await client.OpenAsync();

            // Enable device
            await client.SendEventAsync(ToMessage(deviceInfo));
            // Working with Device Twin
            var twin = await client.GetTwinAsync();
            await client.SetDesiredPropertyUpdateCallback(twinDesiredPropertiesUpdated, client);
            // Set Device Twin properties
            OutputLabel.Text = JsonConvert.SerializeObject(twin);
            var methods = new TwinCollection
            {
                ["Method1"] = "Method 1 Description"
            };
            var properties = new TwinCollection
            {
                ["MyProp"] = "my value 1",
                ["SupportedMethods"] = methods,
            };
            await client.UpdateReportedPropertiesAsync(properties);
            // Handle Cloud-to-Device Methods
            await client.SetMethodHandlerAsync("MyMethod", MyMethod, null);
        }

        private Task<MethodResponse> MyMethod(MethodRequest methodRequest, object context)
        {
            return Task.FromResult(new MethodResponse(200));
        }

        private Task twinDesiredPropertiesUpdated(Microsoft.Azure.Devices.Shared.TwinCollection twins, object context)
        {
            return Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () =>
                    {
                        OutputLabel.Text = JsonConvert.SerializeObject(twins);
                    }).AsTask();
        }

        private Message ToMessage(object data)
        {
            var jsonText = JsonConvert.SerializeObject(data);
            var dataBuffer = System.Text.UTF8Encoding.UTF8.GetBytes(jsonText);
            return new Message(dataBuffer);
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var client = this.client;
            if (client != null)
            {
                await client.CloseAsync();
            }
            base.OnNavigatedFrom(e);
        }
    }
}

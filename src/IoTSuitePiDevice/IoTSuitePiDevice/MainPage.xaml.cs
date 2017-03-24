using IoTSuitePiDevice.Models;
using IoTSuitePiDevice.Models.Properties;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTSuitePiDevice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string DeviceConnectionString = "HostName=demoiotswpark4a826.azure-devices.net;SharedAccessKeyName=device;SharedAccessKey=vnzwO+D5vqOg7e//XAzi63SJMnAus4ycYKqgTYfg20Q=;DeviceId=demo1";
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

            var deviceInfo = new DeviceInfo
            {
                ObjectType = "DeviceInfo",
                ObjectName = "Sample Pi Device",
                Version = "1.0.0",
                DeviceProperties = new DeviceProperties
                {
                    DeviceID = "test1",
                    HubEnabledState = true,
                    BatteryLevel = "100",
                    FirmwareVersion = "1.0.0"
                },
                Telemetry = new List<Telemetry>
                {
                    new Telemetry("Temperature", "Temperature", "double"),
                    new Telemetry("LightLevel", "LightLevel", "double")
                },
            };

            await client.OpenAsync();

            // Enable device
            await client.SendEventAsync(ToMessage(deviceInfo));

            var twin = await client.GetTwinAsync();
            await client.SetDesiredPropertyUpdateCallback(twinDesiredPropertiesUpdated, client);

            OutputLabel.Text = JsonConvert.SerializeObject(twin);

            var properties = new TwinCollection();
            properties["MyProp"] = "my value 1";
            await client.UpdateReportedPropertiesAsync(properties);

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
            await this.client.CloseAsync();
            base.OnNavigatedFrom(e);
        }
    }
}

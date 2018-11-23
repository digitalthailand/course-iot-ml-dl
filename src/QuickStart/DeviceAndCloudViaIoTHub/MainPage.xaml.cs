using Emmellsoft.IoT.Rpi.SenseHat;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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

namespace DeviceAndCloudViaIoTHub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ISenseHat hat;
        private DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();

            Unloaded += MainPage_Unloaded;

            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;

            InitDevice().GetAwaiter();
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            timer?.Stop();
            hat?.Dispose();
            if (hubClient != null)
            {
                hubClient.CloseAsync().ContinueWith(task =>
                {
                    hubClient.Dispose();
                });
            }
            else
            {
                hubClient.Dispose();
            }
        }
        public async Task InitDevice()
        {
            var hat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);

            hat.Display.Fill(Windows.UI.Colors.BlueViolet);
            hat.Display.Update();
            this.hat = hat;

            await ConnectIoTHub();

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    this.timer.Start();
                });
        }

        private bool ToggleOn;
        private void Timer_Tick(object sender, object e)
        {
            var color = ToggleOn
                ? onColor
                : Windows.UI.Colors.Black;
            ToggleOn = !ToggleOn;

            hat.Display.Fill(color);
            hat.Display.Update();
        }

        private DeviceClient hubClient;
        private async Task ConnectIoTHub()
        {
            hubClient = DeviceClient.CreateFromConnectionString(
                "[Your IoT hub device connection string]",
                TransportType.Mqtt);
            await hubClient.OpenAsync();
            await SetupDirectMethods();
            await SetupDeviceTwin();
        }

        private async Task SetupDirectMethods()
        {
            await hubClient.SetMethodHandlerAsync(nameof(ChangeColor), ChangeColor, null).ConfigureAwait(false);
        }

        private async Task SetupDeviceTwin()
        {
            await hubClient.SetDesiredPropertyUpdateCallbackAsync(OnDesiredPropertyChanged, null).ConfigureAwait(false);

            var twin = await hubClient.GetTwinAsync();
            var desiredProperties = twin.Properties.Desired;
            // TODO: Handle desired properties
        }

        private void SendTelemetry(object data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var message = new Message(Encoding.UTF8.GetBytes(json));
            hubClient.SendEventAsync(message);
        }

        private Windows.UI.Color onColor = Windows.UI.Colors.Pink;
        private async Task<MethodResponse> ChangeColor(MethodRequest req, object userContext)
        {
            var colorReq = Newtonsoft.Json.JsonConvert.DeserializeObject<ChangeColorRequest>(req.DataAsJson);
            onColor = Windows.UI.Color.FromArgb(255, colorReq.R, colorReq.G, colorReq.B);
            return new MethodResponse(new byte[0], 200);
        }

        private async Task OnDesiredPropertyChanged(TwinCollection desiredProperties, object userContext)
        {
            // Sending current time as reported property
            TwinCollection reportedProperties = new TwinCollection();
            reportedProperties["DateTimeLastDesiredPropertyChangeReceived"] = DateTime.Now;

            await hubClient.UpdateReportedPropertiesAsync(reportedProperties).ConfigureAwait(false);
        }
    }

    class ChangeColorRequest
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
}

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
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

namespace DemoDevice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DeviceClient deviceClient;

        public MainPage()
        {
            this.InitializeComponent();

            Unloaded += MainPage_Unloaded;

            InitApp().GetAwaiter();
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            deviceClient?.Dispose();
        }

        private async Task InitApp()
        {
            deviceClient = DeviceClient.CreateFromConnectionString("HostName=comiot.azure-devices.net;DeviceId=demo1;SharedAccessKey=wrYYf/gJa52itCl4cVtLa0fxYugPP+2EKlN6H74xKVM=", TransportType.Mqtt);
            await deviceClient.SetMethodHandlerAsync("demo", DemoMethodHandler, null);
            await deviceClient.OpenAsync();
        }

        private async Task<MethodResponse> DemoMethodHandler(MethodRequest req, object context)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ReceiveListBox.Items.Add(string.Format("Received message @{0}", DateTime.Now.ToLongTimeString()));
                var json = Newtonsoft.Json.Linq.JObject.Parse(req.DataAsJson);
                var message = json.Value<string>("Message");
                var count = json.Value<int>("Count");
                for (int i = 0; i < count; i++)
                {
                    ReceiveListBox.Items.Add(message);
                }
            });
            return new MethodResponse(200);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var data = new
            {
                Temperature = Convert.ToDouble(TemperatureTextBox.Text),
                Humidity = Convert.ToDouble(HumidityTextBox.Text)
            };
            var dataJson = JsonConvert.SerializeObject(data);
            var dataBytes = Encoding.UTF8.GetBytes(dataJson);
            deviceClient.SendEventAsync(new Message(dataBytes));

            SendListBox.Items.Add(string.Format("Send @{0}", DateTime.Now.ToLongTimeString()));
            SendListBox.Items.Add(dataJson);
        }
    }
}

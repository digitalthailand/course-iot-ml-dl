using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SenseHatWeatherStation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string ConnectionString = "HostName=st09iot.azure-devices.net;DeviceId=d2;SharedAccessKey=NTDX5Em6XhCP4yXDjVVNSyINPnm0DhKEK3XZjfbOc0A=";
        private DeviceClient client;

        public MainPage()
        {
            this.InitializeComponent();

            Trace.WriteLine("###>>> Starting MainPage ...");

            client = DeviceClient.CreateFromConnectionString(ConnectionString, TransportType.Mqtt);
            InitDevice(); //.GetAwaiter(); //.GetResult();  // do not wait here
        }

        async Task InitDevice()
        {
            await client.OpenAsync();
            await SendMessage(30, 37);
        }

        public async Task SendMessage(double temperature, double humidity)
        {
            var m2send = new
            {
                DeviceId = "d2",
                Temperature = temperature,
                Humidity = humidity
            };
            var formattedM2Send = JsonConvert.ToString(m2send);
            var msgBody = System.Text.UTF8Encoding.UTF8.GetBytes(formattedM2Send);
            var message = new Message(msgBody);

            await client.SendEventAsync(message);
        }
    }
}

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
using Emmellsoft.IoT.Rpi.SenseHat;

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
        private ISenseHat senseHat;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            Trace.WriteLine("###>>> Starting MainPage ...");

            client = DeviceClient.CreateFromConnectionString(ConnectionString, TransportType.Mqtt);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            InitDevice(); //.GetAwaiter(); //.GetResult();  // do not wait here
        }

        private void Timer_Tick(object sender, object e)
        {
            Telemetry();
            blinkSenseHat();
        }

        private bool isOn = false;
        private void blinkSenseHat()
        {
            if (senseHat == null)
            {
                return;
            }

            if (isOn)
            {
                senseHat.Display.Screen[0, 0] = Windows.UI.Colors.Green;
            }
            else
            {
                senseHat.Display.Screen[0, 0] = Windows.UI.Colors.Black;
            }
            isOn = !isOn;

            senseHat.Display.Update();
        }

        private async Task Telemetry()
        {
            if (senseHat.Sensors.HumiditySensor.Update())
            {
                var hum = senseHat.Sensors.Humidity ?? 0;
                var tem = senseHat.Sensors.Temperature ?? 0;

                await SendMessage(tem, hum);
            }
        }

        async Task InitDevice()
        {
            // Init SenseHat
            senseHat = await SenseHatFactory.GetSenseHat();
            // Show that sense hat has been init.
            senseHat.Display.Fill(Windows.UI.Colors.Black);
            senseHat.Display.Update();

            // Connect to IoT Hub
            await client.OpenAsync();

            // start timer
            timer.Start();
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

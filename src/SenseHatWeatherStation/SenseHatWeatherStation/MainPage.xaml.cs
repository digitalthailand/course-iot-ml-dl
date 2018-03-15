using Emmellsoft.IoT.Rpi.SenseHat;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SenseHatWeatherStation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string ConnectionString = "<Your device connections string>";
        private DeviceClient client;
        private ISenseHat senseHat;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            client = DeviceClient.CreateFromConnectionString(ConnectionString, TransportType.Mqtt);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            InitDevice(); //.GetAwaiter(); //.GetResult();  // do not wait here
        }

        async Task InitDevice()
        {
            // Init SenseHat
            senseHat = await SenseHatFactory.GetSenseHat();
            // Show that sense hat has been init.
            senseHat.Display.Fill(Colors.Black);
            senseHat.Display.Update();

            // Connect to IoT Hub
            await client.OpenAsync();

            // start timer
            timer.Start();
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
                senseHat.Display.Screen[0, 0] = Colors.Green;
            }
            else
            {
                senseHat.Display.Screen[0, 0] = Colors.Black;
            }
            isOn = !isOn;

            senseHat.Display.Update();
        }

        private async Task Telemetry()
        {
            if (isOn && senseHat.Sensors.HumiditySensor.Update())
            {
                var hum = senseHat.Sensors.Humidity ?? 0;
                var tem = senseHat.Sensors.Temperature ?? 0;

                await SendMessage(tem, hum);
            }
        }

        public async Task SendMessage(double temperature, double humidity)
        {
            var m2send = new
            {
                DeviceId = "d2",
                Temperature = temperature,
                Humidity = humidity
            };
            var formattedM2Send = JsonConvert.SerializeObject(m2send);
            var msgBody = System.Text.UTF8Encoding.UTF8.GetBytes(formattedM2Send);
            var message = new Message(msgBody);

            await client.SendEventAsync(message);
        }
    }
}

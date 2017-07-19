using Emmellsoft.IoT.Rpi.SenseHat;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherSenseHat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ISenseHat SenseHat;
        public DispatcherTimer timer;

        private IoTServiceClient iotClient = new IoTServiceClient();

        public MainPage()
        {
            this.InitializeComponent();

            InitSenseHat();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private async void InitSenseHat()
        {
            ISenseHat senseHat = await SenseHatFactory.GetSenseHat();
            this.SenseHat = senseHat;

            await iotClient.InitializeIoTServiceConnection();

            SenseHat.Display.Clear();
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            var hat = SenseHat;

            hat.Sensors.HumiditySensor.Update();

            if (hat.Sensors.Humidity.HasValue && hat.Sensors.Temperature.HasValue)
            {
                var hum = hat.Sensors.Humidity.Value;
                var tem = hat.Sensors.Temperature.Value;

                lblMessage.Text = string.Format("Humidity: {0:N}, Temperature: {1:N}", hum, tem);
                iotClient.SendTelemetry(tem, hum);
            }
            else
            {
                lblMessage.Text = "Updating ...";
            }
        }

    }
}

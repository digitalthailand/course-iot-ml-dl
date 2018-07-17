using Emmellsoft.IoT.Rpi.SenseHat;
using GrovePi;
using GrovePi.Sensors;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppG0
{
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        //ISenseHat hat;
        IDHTTemperatureAndHumiditySensor dht;
        IGrovePi grove;
        Pin led = Pin.DigitalPin5;
        Pin buzzer = Pin.DigitalPin2;
        Pin butt = Pin.DigitalPin4;

        public MainPage()
        {
            this.InitializeComponent();

            timer.Interval = TimeSpan.FromMilliseconds(345);
            timer.Tick += Timer_Tick;

            SetupHat();

            Unloaded += MainPage_Unloaded;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            timer?.Stop();
            (grove as IDisposable)?.Dispose();
            (dht as IDisposable)?.Dispose();
        }

        bool ledOn = false;
        private void Timer_Tick(object sender, object e)
        {
            ledOn = !ledOn;
            if (ledOn)
            {
                grove.DigitalWrite(led, 1);
                //hat.Display.Fill(Colors.Beige);
            }
            else
            {
                grove.DigitalWrite(led, 0);
                //hat.Display.Clear();
            }
            var b = grove.DigitalRead(butt);
            grove.DigitalWrite(buzzer, b);

            dht.Measure();

            lbl.Text = string.Format("T: {0}, H: {1}",
                dht.TemperatureInCelsius, dht.Humidity);
            //hat.Display.Update();
            //lbl.Text = DateTime.Now.ToLongTimeString();
        }

        private async Task SetupHat()
        {
            grove = DeviceFactory.Build.GrovePi();
            grove.PinMode(led, PinMode.Output);
            grove.PinMode(buzzer, PinMode.Output);
            grove.PinMode(butt, PinMode.Input);

            dht = DeviceFactory.Build.DHTTemperatureAndHumiditySensor(Pin.DigitalPin7, GrovePi.Sensors.DHTModel.Dht11);

            lbl.Text = "Ready! 1.0.1";

            //hat = await SenseHatFactory.GetSenseHat() // .Configure
            //            .ConfigureAwait(false);
            //hat.Display.Fill(Colors.Azure);
            //hat.Display.Update();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => timer.Start());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lbl.Text = "Teerachai";
        }
    }
}

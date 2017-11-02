using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DemoSenseHat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private Emmellsoft.IoT.Rpi.SenseHat.ISenseHat hat;
        private Microsoft.Azure.Devices.Client.DeviceClient device;

        private const string DeviceConnectionString = "HostName=prelab.azure-devices.net;DeviceId=spi;SharedAccessKey=n8wt6OghOciC5hiaBVOwmfUTuASJTywS7gy4t1ad2Ec=";

        public MainPage()
        {
            this.InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            device = Microsoft.Azure.Devices.Client.DeviceClient.CreateFromConnectionString(DeviceConnectionString);

            InitThings();

            this.Unloaded += MainPage_Unloaded;
        }

        private async void InitThings()
        {
            hat = await Emmellsoft.IoT.Rpi.SenseHat.SenseHatFactory.GetSenseHat();

            await device.OpenAsync();

            hat.Display.Fill(Windows.UI.Colors.Navy);
            hat.Display.Update();

            timer.Start();
        }

        private int msgid = 100;
        private void Timer_Tick(object sender, object e)
        {
            lbl.Text = DateTime.Now.ToString("T");

            hat.Sensors.HumiditySensor.Update();

            if (hat.Sensors.Humidity.HasValue && hat.Sensors.Temperature.HasValue)
            {
                device.SendEventAsync(ToMessage(new
                {
                    MessageId = ++msgid,
                    DeviceId = "spi",
                    Temperature = hat.Sensors.Temperature.Value,
                    Humidity = hat.Sensors.Humidity.Value,
                }));
            }
        }

        private Microsoft.Azure.Devices.Client.Message ToMessage(object data)
        {
            var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var dataBuffer = System.Text.UTF8Encoding.UTF8.GetBytes(jsonText);
            return new Microsoft.Azure.Devices.Client.Message(dataBuffer);
        }

        private async void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            await device.CloseAsync();
            device.Dispose();
        }
    }
}

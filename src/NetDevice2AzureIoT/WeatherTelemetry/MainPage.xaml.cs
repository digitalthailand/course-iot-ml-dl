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

namespace WeatherTelemetry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IoTServiceClient iotClient;
        private SensorReader sensor;

        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            iotClient = new IoTServiceClient();
            sensor = new SensorReader();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private async void InitThings()
        {
            await iotClient.InitializeIoTServiceConnection();
            await sensor.InitializeSenseHat();
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            // TODO: Implement
        }
    }
}

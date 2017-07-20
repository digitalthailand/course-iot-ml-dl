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

namespace FillColorControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ISenseHat SenseHat;

        private IoTServiceClient iotClient;

        public MainPage()
        {
            this.InitializeComponent();

            iotClient = new IoTServiceClient(SetFillColor);

            InitSenseHat();
        }

        private async void InitSenseHat()
        {
            ISenseHat senseHat = await SenseHatFactory.GetSenseHat();
            this.SenseHat = senseHat;

            await iotClient.InitializeIoTServiceConnection();

            SenseHat.Display.Clear();
            SenseHat.Display.Fill(Windows.UI.Colors.Black);
            SenseHat.Display.Update();
        }

        private void SetFillColor(Windows.UI.Color color)
        {
            SenseHat.Display.Clear();
            SenseHat.Display.Fill(color);
            SenseHat.Display.Update();
        }
    }
}

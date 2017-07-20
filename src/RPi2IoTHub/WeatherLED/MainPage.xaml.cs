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

namespace WeatherLED
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ISenseHat SenseHat;
        public DispatcherTimer timer;

        private int seed4led;

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

            SenseHat.Display.Clear();
            timer.Start();

            lblMessage.Text = "Running ...";
        }

        private void Timer_Tick(object sender, object e)
        {
            FillDisplay();
        }

        private void FillDisplay()
        {
            SenseHat.Display.Clear();

            var seed = ++seed4led % 64; // 8 * 8 = 64

            var count = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ++count;
                    if (count == seed)
                    {
                        SenseHat.Display.Screen[x, y] = Windows.UI.Colors.Green;
                    }
                    else
                    {
                        SenseHat.Display.Screen[x, y] = Windows.UI.Colors.Black;
                    }
                }
            }

            SenseHat.Display.Update();
        }
    }
}

using GHIElectronics.UWP.Shields;
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

namespace HelloWorldFezHat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FEZHAT hat;
        private DispatcherTimer timer;
        private bool isLightOn = false;

        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.hat = await FEZHAT.CreateAsync();

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.25);
            timer.Tick += Timer_Tick;
            timer.Start();

            this.timer = timer;
        }

        private void Timer_Tick(object sender, object e)
        {
            this.isLightOn = !this.isLightOn;
            if (this.isLightOn)
            {
                this.hat.D2.Color = FEZHAT.Color.Green;
            }
            else
            {
                this.hat.D2.TurnOff();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.timer.Stop();
            this.hat.D2.TurnOff();
            this.hat.Dispose();

            base.OnNavigatedFrom(e);
        }
    }
}

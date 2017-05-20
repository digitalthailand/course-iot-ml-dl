using Emmellsoft.IoT.Rpi.SenseHat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Workshop0
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly Random Random = new Random();
        private ColorMode _currentMode;
        public ISenseHat SenseHat;
        public DispatcherTimer timer;

        private enum ColorMode
        {
            SoftRandom,
            HardRandom,
            Sparkling,
            Blocks,
            Unicolor
        }

        public MainPage()
        {
            this.InitializeComponent();
            InitSenseHat();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            FillDisplay();

            SenseHat.Display.Update();

            // Should the drawing mode change?
            if (SenseHat.Joystick.Update() && (SenseHat.Joystick.EnterKey == KeyState.Pressing))
            {
                // The middle button is just pressed.
                SwitchToNextColorMode();
            }
        }

        private async void InitSenseHat()
        {
            ISenseHat senseHat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);
            this.SenseHat = senseHat;
        }


        private void SwitchToNextColorMode()
        {
            _currentMode++;

            if (_currentMode > ColorMode.Unicolor)
            {
                _currentMode = ColorMode.SoftRandom;
            }

            SenseHat.Display.Clear();
        }

        private void FillDisplay()
        {
            switch (_currentMode)
            {
                case ColorMode.SoftRandom:
                    FillDisplaySoftRandom();
                    break;

                case ColorMode.HardRandom:
                    FillDisplayHardRandom();
                    break;

                case ColorMode.Sparkling:
                    FillDisplaySparkling();
                    break;

                case ColorMode.Blocks:
                    FillDisplayBlocks();
                    break;

                case ColorMode.Unicolor:
                    FillDisplayUnicolor();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FillDisplaySoftRandom()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Color pixel = Color.FromArgb(
                        255,
                        (byte)Random.Next(256),
                        (byte)Random.Next(256),
                        (byte)Random.Next(256));

                    SenseHat.Display.Screen[x, y] = pixel;
                }
            }
        }

        private void FillDisplayHardRandom()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Color pixel = Color.FromArgb(
                        255,
                        (byte)(Random.Next(2) * 255),
                        (byte)(Random.Next(2) * 255),
                        (byte)(Random.Next(2) * 255));

                    SenseHat.Display.Screen[x, y] = pixel;
                }
            }
        }

        private void FillDisplaySparkling()
        {
            const double probabilityForNewSparkle = 0.99; // 1=always new sparkle, 0=never.
            const double oldSparkleFadeRate = 0.75;       // The decrease in percentage of the intensity from one frame to another (0.5 = 50 % decrease).

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    bool sparkle = Random.NextDouble() >= probabilityForNewSparkle;

                    Color pixel;
                    if (sparkle)
                    {
                        // This pixel should start a new sparkle.
                        pixel = Colors.White;
                    }
                    else
                    {
                        // Get the last color of this pixel.
                        byte lastIntensity = SenseHat.Display.Screen[x, y].R;
                        if (lastIntensity <= 10)
                        {
                            // Quite dark -- be pitch black.
                            pixel = Colors.Black;
                        }
                        else
                        {
                            // Turn down the intensity.
                            byte newIntensity = (byte)Math.Round(lastIntensity * oldSparkleFadeRate);

                            pixel = Color.FromArgb(255, newIntensity, newIntensity, newIntensity);
                        }
                    }

                    SenseHat.Display.Screen[x, y] = pixel;
                }
            }
        }

        private void FillDisplayBlocks()
        {
            for (int y = 0; y < 8; y += 2)
            {
                for (int x = 0; x < 8; x += 2)
                {
                    Color pixel = Color.FromArgb(
                        255,
                        (byte)(Random.Next(2) * 255),
                        (byte)(Random.Next(2) * 255),
                        (byte)(Random.Next(2) * 255));

                    SenseHat.Display.Screen[x, y] = pixel;
                    SenseHat.Display.Screen[x + 1, y] = pixel;
                    SenseHat.Display.Screen[x, y + 1] = pixel;
                    SenseHat.Display.Screen[x + 1, y + 1] = pixel;
                }
            }
        }

        private void FillDisplayUnicolor()
        {
            SenseHat.Display.Fill(Color.FromArgb(
                255,
                (byte)(Random.Next(2) * 255),
                (byte)(Random.Next(2) * 255),
                (byte)(Random.Next(2) * 255)));
        }
    }
}

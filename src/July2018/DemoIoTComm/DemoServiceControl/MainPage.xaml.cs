using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace DemoServiceControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ServiceClient serviceClient;

        public MainPage()
        {
            this.InitializeComponent();

            Unloaded += MainPage_Unloaded;

            InitApp().GetAwaiter();
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            serviceClient?.Dispose();
        }

        private async Task InitApp()
        {
            // TODO: Replace connection string
            serviceClient = ServiceClient.CreateFromConnectionString("HostName=comiot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=NiHQ0aP/8RSoOUEfmQItNU4oAdnG+RUgoXOUt/jlYrM=");
        }

        private async void InvokeButton_Click(object sender, RoutedEventArgs e)
        {
            var payload = new
            {
                Message = Convert.ToString(MessageTextBox.Text),
                Count = Convert.ToDouble(CountTextBox.Text)
            };
            var payloadJson = JsonConvert.SerializeObject(payload);
            var c2dMethod = new CloudToDeviceMethod("demo");
            c2dMethod.SetPayloadJson(payloadJson);

            LogListBox.Items.Add(string.Format("Invoke method @{0}", DateTime.Now.ToLongTimeString()));
            try
            {
                var result = await serviceClient.InvokeDeviceMethodAsync("demo1", c2dMethod);
                LogListBox.Items.Add(string.Format("Result Status: {0}", result.Status));
            }
            catch (Exception ex)
            {
                LogListBox.Items.Add(string.Format("Error: {0}", ex));
            }
        }
    }
}

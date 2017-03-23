using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using System.Threading.Tasks;
using GHIElectronics.UWP.Shields;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace HelloBackground
{
    public sealed class StartupTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var hat = await FEZHAT.CreateAsync();

            for (int i = 0; i < 100; i++)
            {
                hat.D3.Color = FEZHAT.Color.Blue;
                SillyWait();
                hat.D3.TurnOff();
                SillyWait();
                hat.D3.Color = FEZHAT.Color.Red;
                SillyWait();
                hat.D3.TurnOff();
                SillyWait();
            }
            hat.Dispose();

            deferral.Complete();
        }

        public void SillyWait()
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {

                }
            }
        }
    }
}

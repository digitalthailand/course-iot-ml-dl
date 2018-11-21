# Quick Start

## UWP and Sense Hat

### UWP Simple UI

1. Replace XAML code section `Grid`,

    ```xaml
    <Grid>

    </Grid>
    ```

    with the following code:

    ```xaml
    <StackPanel Orientation="Vertical">
        <StackPanel>
            <TextBlock Text="Hello" />
            <TextBox x:Name="TemperatureTextBox" Margin="5" />
        </StackPanel>
        <StackPanel>
            <TextBlock Text="Hello" />
            <TextBox x:Name="HumidityTextBox" Margin="5" />
        </StackPanel>
        <StackPanel>
            <TextBlock x:Name="TimeTextBlock" Text="Show Time" />
            <Button x:Name="ActionButton" Content="OK" />
        </StackPanel>
    </StackPanel>
    ```

2. This is the first version of MainPage

    ```csharp
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;

            InitDevice().GetAwaiter();
        }

        public async Task InitDevice()
        {
            this.timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
        }
    }
    ```

### Sense Hat

```csharp
public sealed partial class MainPage : Page
{
    private ISenseHat hat;
    private DispatcherTimer timer = new DispatcherTimer();

    public MainPage()
    {
        this.InitializeComponent();

        timer.Interval = TimeSpan.FromSeconds(0.5);
        timer.Tick += Timer_Tick;

        InitDevice().GetAwaiter();
    }

    public async Task InitDevice()
    {
        var hat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);

        hat.Display.Fill(Windows.UI.Colors.BlueViolet);
        hat.Display.Update();
        this.hat = hat;

        //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        //    () =>
        //    {
                this.timer.Start();
            //});
    }

    private bool ToggleOn;
    private void Timer_Tick(object sender, object e)
    {
        var color = ToggleOn 
            ? Windows.UI.Colors.Gold 
            : Windows.UI.Colors.Black;
        ToggleOn = !ToggleOn;

        hat.Display.Fill(color);
        hat.Display.Update();
    }
}
```

### Sending Telemetry Events

```csharp
private DeviceClient hubClient;
private async Task ConnectIoTHub()
{
    hubClient = DeviceClient.CreateFromConnectionString(
        "[Your IoT hub device connection string]",
        TransportType.Mqtt);
    await hubClient.OpenAsync();
}
private void SendTelemetry(object data)
{
    var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
    var message = new Message(Encoding.UTF8.GetBytes(json));
    hubClient.SendEventAsync(message);
}
```

### Cleaning Up Resource Usage

Unloaded Sample:

```csharp
private void MainPage_Unloaded(object sender, RoutedEventArgs e)
{
    hat?.Dispose();
    if (hubClient != null)
    {
        hubClient.CloseAsync().ContinueWith(task =>
        {
            hubClient.Dispose();
        });
    }
    else
    {
        hubClient.Dispose();
    }
}
```
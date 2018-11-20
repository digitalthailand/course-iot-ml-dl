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

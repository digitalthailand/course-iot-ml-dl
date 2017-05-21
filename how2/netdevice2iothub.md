# Connecting an IoT Device to Azure IoT Hub
## .NET (UWP)


* Update NuGet references
* NuGet `Microsoft.Azure.Devices.Client`
* Add a class `SensorReader` (SensorReader.cs)
* Add a class `IoTServiceClient` (IoTServiceClient.cs)
* Add using statements

    ```
    using Microsoft.Azure.Devices.Client;
    using Newtonsoft.Json;
    ```

* Add statements

    ```
    private static readonly string DeviceId = "{device Id}";
    private static readonly string iotHubUri = "{iot hub hostname}";
    private static readonly string deviceKey = "{device key}";
    private readonly string DeviceConnectionString = $"HostName={iotHubUri};DeviceId={DeviceId};SharedAccessKey={deviceKey}";
    private DeviceClient deviceClient;
    ```

* 
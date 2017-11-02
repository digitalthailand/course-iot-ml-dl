# IoT, Machine Learning and Data Analytics
## Course Companion

> ATTENTION: SAS Key:  
HostName=labrealdevice1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=9cHajFi7QxDrl+8+KRafEJueW16OGcF4mSUtdYxn/vI=

> ATTENTION: The sample data for the Eastern CodeFest can be downloaded from
[http://tinyurl.com/eastern-codefest-sample-data](http://tinyurl.com/eastern-codefest-sample-data)
* [Completed Sample Data](http://tinyurl.com/yagfk28o)
* [Questions for Software Teams](https://easterncodefest.blob.core.windows.net/sample-data/sw.pdf)
* [Questions for Hardware Teams](https://easterncodefest.blob.core.windows.net/sample-data/hw.pdf)

> ATTENTION: Please check out http://powerbi.com (Workshop 4).
For ones who don't have work email account (email which is not gmail or hotmail),
please send a fb message to Digital Thailand Club after 15.00.
I'll give you one (the email which you can use to register to the power bi).

> SUGGESTION: http://github.com might be helpful for your work as a team.

> WARNING: Do not forget to DELETE your resources in your Azure Subscription!!

> Digital Thailand Club: https://www.facebook.com/digitalthailandclub

## FAQ
**Q:** Can I use Power BI in my application (XAML-based)?  
**A:** Yes, please check out these links for more info  
* [Power BI in UWP](http://community.powerbi.com/t5/Desktop/Power-BI-Embedded-Integrate-a-report-into-a-UWP-app/td-p/59360)
* [Using Power BI in WebView](https://gist.github.com/itsananderson/14a179174ea65e246ce7)

**Q:** Is there any quick and easy way to simulate the IoT Device use for the problems in `Eastern CodeFest`?  
**A:** Yes there're a couple of suggestions:
1. Use iothub-explorer
1. Use the Raspberry Pi web simulator
1. Use the following hack page for testing [http://easterncodefest.azurewebsites.net/](http://easterncodefest.azurewebsites.net/)

**Q:** How to embed a Power BI Dashboard into the web app?  
**A:** Please check out the following links  
* [Power BI Client](https://microsoft.github.io/PowerBI-JavaScript/)
* [Integrate a dashboard into an app](https://powerbi.microsoft.com/en-us/documentation/powerbi-developer-integrate-dashboard/)
* [Power BI Command line tool](https://github.com/Microsoft/PowerBI-cli)

## Hands-out
* Introducing IoT
* [Chapter 1 - Azure IoT Services](slides/m01_azure_iot.pdf)
* [Chapter 2 - Cloud Platform](slides/m02_cloud.pdf)
* [Chapter 3 - Cloud Services](slides/m03_cloud15.pdf)
* [Chapter 4 - Storage and Cognitive Services](slides/m04_storage.pdf)
* [Chapter 5 - Machine Learning](slides/m05_machine_learning.pdf)
* [Chapter 6 - Stream Analytics](slides/m06_stream_analytics.pdf)

## Workshop Manual
* [The workshop manual page](https://www.gitbook.com/book/tlaothong/azure-iot-workshop)

## Workshop 0
* [Azure IoT Suite](http://www.azureiotsuite.com/)

## Workshop 1
* [Connect Raspberry Pi to Azure IoT Hub](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-raspberry-pi-kit-node-get-started.html)

## Workshop 1b
* [Using Raspberry Pi online simulator](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-raspberry-pi-web-simulator-get-started.html)

## Workshop 2
* [Manage Cloud Device message with IoT Hub Explorer](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-explorer-cloud-device-messaging.html)

## Workshop 3
* [Save messages to Azure Storage](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-store-data-in-azure-table-storage.html)

## Workshop 4
* [Data Visualization in Power BI](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-live-data-visualization-in-power-bi.html)

## Workshop 5
* [Data Visualization with Web App](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-live-data-visualization-in-web-apps.html)

## Workshop 6a
* [Machine Learning Experiment](https://docs.microsoft.com/en-us/azure/machine-learning/studio/create-experiment)

## Workshop 6
* [Weather forcast using Azure Machine Learning](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-weather-forecast-machine-learning.html)

## Workshop 7
* [Device Management using IoT Hub Explorer](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-device-management-iothub-explorer.html)

## Workshop 8
* [Remote Monitoring and Notifications](https://tlaothong.gitbooks.io/azure-iot-workshop/content/iot-hub-monitoring-notifications-with-azure-logic-apps.html)

## Useful Links
* [How to register for a Free Account - http://bit.ly/2hNfJA2](http://bit.ly/2hNfJA2)
* [Visual Studio Dev Essentials](https://www.visualstudio.com/dev-essentials/)
* [Microsoft Azure](https://azure.com)
* [Azure IoT Documentation](https://docs.microsoft.com/en-us/azure/#pivot=services&panel=iot)
* [Windows IoT (Dev Center)](https://developer.microsoft.com/windows/iot)
    * [Supported Devices](https://developer.microsoft.com/windows/iot/explore/deviceoptions)
* [Microsoft IoT Cloud Platform](https://www.microsoft.com/en-us/cloud-platform/internet-of-things)
* [Azure IoT Suite Learning Path](https://azure.microsoft.com/en-us/documentation/learning-paths/iot-suite/)
* [Azure IoT Suite - Remote Monitoring Source Code](https://github.com/Azure/azure-iot-remote-monitoring)
* [Azure IoT Suite - Predictive Maintenance Source Code](https://github.com/Azure/azure-iot-predictive-maintenance)
* [FEZ HAT](https://www.ghielectronics.com/catalog/product/500)
    * [Developer's Guide](https://www.ghielectronics.com/docs/329/fez-hat-developers-guide)
    * [Schematic (pdf)](http://www.ghielectronics.com/downloads/schematic/FEZ_HAT_SCH.pdf)
* [Sense HAT](https://www.hackster.io/laserbrain/windows-iot-sense-hat)
    * [SDK (github)](https://github.com/emmellsoft/RPi.SenseHat)
* [Microsoft IoT Samples](https://github.com/ms-iot/samples)
* [Using Webcam in UWP (also Raspberry Pi)](https://developer.microsoft.com/en-us/windows/iot/samples/webcamapp)

### Lab Accelarator
* Connect devices to IoT Hub
    * Using Node.js
        * [Using Node.js (simulated)](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-node-node-device-management-get-started)
        * Also [using Node.js with Raspberry Pi](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-raspberry-pi-kit-node-get-started)
    * Using .NET
        * [Official Guide (simulated)](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-csharp-csharp-getstarted)
        * [Using .NET (device)](how2/netdevice2iothub.md)

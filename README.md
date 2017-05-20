# IoT, Machine Learning and Data Analytics
## Course Companion

## Hands-out
* Introducing IoT
* [Chapter 1 - Azure IoT Services](slides/m01_azure_iot.pdf)
* [Chapter 2 - Cloud Platform](slides/m02_cloud.pdf)
* [Chapter 3 - Cloud Services](slides/m03_cloud15.pdf)
* [Chapter 4 - Storage and Cognitive Services](slides/m04_storage.pdf)
* [Chapter 5 - Machine Learning](slides/m05_machine_learning.pdf)
* [Chapter 6 - Stream Analytics](slides/m06_stream_analytics.pdf)

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

## Workshop 0
* Download the source from `src\LabWorkshop0`

## Workshop 1
* Log into [https://www.azureiotsuite.com](https://www.azureiotsuite.com)

## Workshop 2 - Machine Learning
* Please follow instructions from [Machine Learning Workshop](https://docs.microsoft.com/en-us/azure/machine-learning/machine-learning-create-experiment)

[//]: # (https://tlaothong.gitbooks.io/azure-iot-workshop/content/azure-ml-studio.html)

## Workshop 3 - Cognitive Services
* Please follow instructions from [Cognitive Service Workshop](https://tlaothong.gitbooks.io/azure-iot-workshop/content/cognitive-services.html)

[//]: # (https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-recommendations-quick-start)

## Workshop 4 - Stream Analytics and Azure ML
* Please follow instructions from [Using Azure Stream Analytics and Azure Machine Learning](https://docs.microsoft.com/en-us/azure/stream-analytics/stream-analytics-machine-learning-integration-tutorial)

## Workshop 5 - Stream Analytics to Power BI
* Please follow instructions from [Sensor data to Power BI](https://gallery.cortanaintelligence.com/Tutorial/Sensor-Data-Analytics-with-ASA-and-Power-BI-2)

## Workshop 6 - Azure Functions and Cognitive Services
* Please follow instructions from [Fun with Azure Functions](http://martinabbott.azurewebsites.net/2016/06/11/fun-with-azure-functions-and-the-emotion-api/)

## Errata
In Workshop 4 we must use the query code as follows:

```
WITH sentiment AS (  
  SELECT text, sentiment(text) as result from datainput  
)  

Select text, result.[Sentiment], result.[Score]  
Into testoutput  
From sentiment
```
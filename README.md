# AirlineFlightDataService

The programming task is about creating a small application which processesairline flight data. The application will watch a folder where new event data files will be placed by some other system (Out of scope for this task) and do validation before it will enter our Big Data platform.
The application will: (1) read the JSON in the event data file, (2) create a copy of the file in a different folder called “Raw” and (3) validate the content of the events and write individual events into a either “Exception” or “Curated” folder. (4) If the file is invald json file formate, it will be copy to exception folder and log the error details in console.

The resulting folder structure will look like this:
 
The out filename: {EVENTTYPE}-{TIMESTAMP}.json. Timestamp is the start processing DateTime in Ticks.
For this exercise the application supports two types of events (See below), but the entire system will support hundreds. Event Data Files can contain multiple event types within the same batch.
For each batch we are expecting logs to be created to provide support and overview information:
•	List of event types that were processed, incl Count of events per type
•	Total duration for processing each batch
•	Count of failed events
•	List of the IDs of the failed events.


## AirlineFlightDataService Highlight

AirlineFlightDataService project is a .Net Core 2.2 console app. It is designed to be Decoupling application to make sure it is SOLD.
Rules can be easliy added and doesn't need modify existing code. Same with the handlers. The core of this application is that there is
no need to modify existing code when a new type is added. The only thing need to be done is add a config section of that type name with
all folder path into appsetting.json file, so it follows open for extension and close for modification principle. Using DI is another way to keep everything follows Single responsibility principle. For example, the watcher only need to watch the folder and pass the file to the handler, and it is not knowing how it will be hanldered. Same with the processor, it does not know how to process the event, it only knows how to pass the event. Using NLog is very easy to config the log level and log target. The log target can be text file or console. Using DI is also very helpful for unit test. It is easy to use moq to mock the response.


## Built With

* [NLog](https://github.com/nlog/nlog/wiki) - The log framework used
* [Ninject](https://github.com/ninject/Ninject/wiki) - Dependency injection framework
* [.Net Core 2.2](https://docs.microsoft.com/en-us/dotnet/core/) - Framework used to build this console app


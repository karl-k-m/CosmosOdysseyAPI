# Cosmos Odyssey API

This is the back end repository for Cosmos Odyssey, a test project which using ASP.NET backend and React frontend.  
For front end code, visit https://github.com/karl-k-m/cosmosodyssey/  

# Basic Documentation 

## Models
| **Model Name**    | **Features**                                                                                                         |
|-------------------|----------------------------------------------------------------------------------------------------------------------|
| Flight            | FlightID, CompanyName, Origin, Destination, Distance, Price, DepartureTime, ArrivalTime, ValidUntil, ValidityCounter |
| TravelReservation | ReservationID, PassengerFirstName, PassengerLastName, Distance, Duration, Price, ValidityCounter                     |
| ReservationFlight | ReservationFlightId, ReservationID, FlightID, ValidityCounter                                                        |

For details on models and their features, check the Models directory in the source code.  

## Data Source
Data is pulled from https://cosmos-odyssey.azurewebsites.net/api/v1.0/TravelPrices  
The data is processed by UpdateFlightsBackgroundService.cs in the BackgroundProcesses directory. This background process checks for new price lists and updates the database accordingly.

I have purposefully ignored the "id" feature for all json attributes except flights. This is because they are randomized each time (including, for instance, a company called "SpaceX" will have different IDs for two different pricelists despite seemingly being the same company) and do not provide any meaningful information.  

## Issues
There are a few issues which are known to me which I have not addressed at this time. They are as follows:  
* Request validation is done only in the front-end.
* Exception handling is spotty.
* I have not implemented a rate limit.

# Deployment

For my end, this project is deployed as follows:  
**Front End** - Front end is deployed on Github Pages (https://karl-k-m.github.io/cosmosodyssey/)  
**Back End** - Back end is composed of a PostgreSQL database and the actual API, both of which are hosted on Azure. The website is quite slow because I'm using the cheapest plan available.

To deploy this webapp, you should do as follows:  
1. Deploy a database on your hosting service of choice. Make sure you have the correct schema (dotnet-ef tool makes this quite easy).
2. Deploy the backend on your hosting service of choice. Prior to this, change the SQL connection string in Data/ApiContext.cs accordingly.
3. Deploy the frontend on your hosting service of choice. Prior to this, change "homepage" in package.json

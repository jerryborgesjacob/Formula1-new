# Formula1 Points Manager

## Introduction
Formula 1 Manager is a appplication that helps you manage the Driver, Team and Racetrack data in a season.The application is built using C#. WebAPI and MVC to create an application that has CRUD functionality, user authorization and a visually appealing design.

## Features
- Driver Management: Manage Drivers and the points they score in a season.
- Team Management: Manage Teams, Engine Suppliers, Team scored and Driver associated with the team.
- Race Track Management: Manage Data about race tracks used in the season.
- List, Add, Delete and Update details in the database using the forms in the webpage.

## Database Structure
- Driver Table: DriverId, DriverName and DriverPoints
- Team Table: TeamId, TeamName, EngineSupplier, TeamPoints and DriverId (Foreign Key)
- Racetrack table: TrackId, TrackName, TrackLength, Country

## NOTES
1. The application requires you to register a new account to access the features. Click on Register in the NavBar, and create a new account. Use the same credentials to login.
2. The application has working CRUD Functionality. However, due to some issue with foreign key constraints, Add and Delete in Driver and Team pages may result in an error. The app has no issues when the data is added manually into the database, but runs into some issue when done through the application.
3. The CRUD in the Racetrack page works as intended.

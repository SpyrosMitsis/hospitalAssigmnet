# Object Oriented Assignment made with ASP.NET Core MVC and Entity Framework README

This is a Core MVC application made for the course "OBJECT ORIENTED PROGRAMMING" SWE4005

This project demonstrates the usage of ASP.NET Core MVC with Entity Framework, Microsoft SQL Server, single table inheritance for employee types, and relationships between entities. It also includes unit tests, an in-memory database for repository tests, fake data generation using Bogus, and logging with Serilog.

## Getting Started

1. Run the `mitsisDbMigration.sql` this creates the database schema and adds the seed data.
2. Configure the database connection and other settings in the `appsettings.json` file, if it does not work  try <https://www.connectionstrings.com/sql-server/>.
4. To Configure Serilog to Log on a file, inside the `appsettings.json` file change the `"path": "C:\\Users\\mitsi\\Documents\\C#\\New folder\\hospitals\\log.txt",` to a spefic path on your
	computer
5. Build and run the application.
5. Use the provided unit tests to verify the functionality of the controllers and repositories.

## Technologies Used

- ASP.NET Core MVC
- Entity Framework
- Microsoft SQL Server
- Single Table Inheritance
- Unit Testing
- In-Memory Database (For Testing)
- Bogus (Fake Data Generation)
- Serilog (Logging)

## Project Structure

The project follows the standard structure of an ASP.NET Core MVC application.

- `Controllers/`: Contains the controllers responsible for handling incoming requests and returning responses.
- `Interfaces/`: Contains the Interfaces for the repositories that handle data access.
- `Models/`: Contains the entity models representing the database tables, including the employee, doctor, nurse, room, patient, and address entities.
- `Repository/`: Contains the repositories that handle data access and database operations.
- `Views/`: Contains the Views that display data.
- `Hospital.Tests/`: Contains the unit tests for the controllers and repositories, using an in-memory database for testing.

## Database Configuration

The application uses Microsoft SQL Server as the database provider. The connection string and other database configurations can be found in the `appsettings.json` file.

## Single Table Inheritance

The employee type, specifically for doctors and nurses, is implemented using single table inheritance. The common properties for all employees are stored in the "Employee" table, and additional properties for doctors and nurses are stored in their respective tables.

- Doctors have a list of associated patients.
- Nurses have a list of associated rooms.

## Unit Testing

The project includes unit tests to ensure the functionality of controllers and repositories. The tests are located in the `Hospital.Test/` directory and can be run using NUnit.

## In-Memory Database

For testing purposes, an in-memory database is used to isolate the tests from the actual database. This allows for faster and more reliable testing without affecting the production database.

## Fake Data Generation

To generate fake data for testing and development, the project uses the Bogus library, which simplifies the creation of realistic and randomized data. This ensures that the application can handle various scenarios and edge cases during testing.

## Logging
 
The project utilizes Serilog as the logging framework. Serilog is integrated with ASP.NET Core MVC's existing logging infrastructure and provides flexible and configurable logging options. Logging statements can be found throughout the codebase to capture important events and information.


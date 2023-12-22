# Event Management System

The Event Management System is a web-based application that allows users to create, manage, and participate in events. This project was built using **ASP.NET Core** and provides a **RESTful APIs** for the frontend to interact with.

\
Before users can create or book events, they have to register. The registration process collects necessary information such as name and email, and ensures the password is securely stored.

## Features

- Event Creation: Registered users can create events. When creating an event, users should provide details such as the event name, description, location, date and time, and the number of available tickets.
- Event Management: Event creators can update the details of their events or delete their events if necessary.
- Event Participation: Users can view the list of available events and book tickets for the events they are interested in. The system updates the number of available tickets accordingly.
- Ticket Management: Users can view the tickets they have booked and have the option to cancel their bookings if necessary.

## Requirements

The project requires the following:

- .NET6 Runtime
- MySQL database server [ **This is not a requirement if you are on branch <ins>SQLite</ins>**&nbsp;]

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Build the solution to restore the NuGet packages.
4. Configure the connection string in **appsettings.json** file [ **This step is not required if you are on branch <ins>SQLite</ins>**&nbsp;]
5. Create (or update) the database by executing **update-database** command in Package Manager Console.
6. Run the application.

## Notes

- You can test the application and review the APIs at the following URL: `/swagger/index.html`
- The project uses JWT Authentication schema.

##

I hope this helps you. Let me know if you have any other questions!

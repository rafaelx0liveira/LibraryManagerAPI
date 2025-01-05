# LibraryManagerAPI

## Overview
LibraryManagerAPI is a robust library management system that allows administrators to manage users, books, and loans efficiently. It includes an API for CRUD operations, a notification system for overdue loans, and clear separation of responsibilities following clean architecture principles.

---

## Features
- **User Management**: CRUD operations for managing user details.
- **Book Management**: CRUD operations for adding, updating, and managing book inventory.
- **Loan System**:
  - Loan registration with validations for user and book availability.
  - Notification system for overdue loans using Azure Functions.
  - Loan status updates handled automatically.
- **Email Notifications**: Sends HTML-formatted emails using SendGrid.

---

## Architecture
The project is designed following the principles of Clean Architecture:

- **Application Layer**: Contains business logic encapsulated in `UseCases`.
- **Domain Layer**: Includes core entities, enums, and value objects.
- **Infrastructure Layer**: Handles external services such as email and database interactions.
- **Presentation Layer**: Exposes RESTful APIs to the clients.

---

## Tech Stack
- **Framework**: .NET 6
- **Database**: MySQL
- **Email Service**: SendGrid
- **Cloud**: Azure Functions for notification services
- **Dependency Injection**: Built-in .NET DI container
- **Logging**: Integrated with .NET logging system

---

## Setup Instructions

### Prerequisites
- .NET 6 SDK
- MySQL Database
- SendGrid API Key
- Azure Functions Core Tools (if running locally)

### Configuration
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/LibraryManagerAPI.git
   cd LibraryManagerAPI
   ```

2. Update the `appsettings.json` file:
   ```json
   {
     "ConnectionStrings": {
       "MySqlConnectionString": "Server=localhost;Database=librarymanager;Uid=root;Pwd=yourpassword;"
     },
     "SendGrid": {
       "ApiKey": "your-sendgrid-api-key"
     }
   }
   ```

3. Apply migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

---

## Key Endpoints

### User Management
- **Create User**: `POST /api/users`
- **Get User by Email**: `GET /api/users/{email}`
- **Update User**: `PUT /api/users/{id}`
- **Delete User**: `DELETE /api/users/{id}`

### Book Management
- **Add Book**: `POST /api/books`
- **Get Book by ISBN**: `GET /api/books/{isbn}`
- **Update Book**: `PUT /api/books/{id}`
- **Delete Book**: `DELETE /api/books/{id}`

### Loan Management
- **Register Loan**: `POST /api/loans`
- **Get Loans by User**: `GET /api/loans/by-user/{email}`

---

## Azure Functions
The `NotifyOverdueLoansFunction` performs the following tasks:
1. Updates overdue loans in the database.
2. Sends email notifications to users with overdue loans.

### Timer Trigger
The function is triggered every 24 hours:
```cron
0 0 * * * *
```

---

## Email Notifications
- **Template Path**: `Infrastructure/Templates/OverdueLoanNotification.html`
- **Placeholders**:
  - `{{UserName}}`: User's name
  - `{{BookTitle}}`: Title of the overdue book
  - `{{ReturnDate}}`: Due date of the loan

---






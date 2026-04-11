# OfficeBooker API
OfficeBooker API is a robust backend solution for managing office spaces and desk reservations. Built with .NET 10, it offers a comprehensive set of features to streamline office management and enhance the booking experience for workers.
## 🚀 Key Features

* **Office Management:** Full CRUD operations for managing office spaces and desk capacities.
* **Reservation System:** Advanced booking logic with overlapping date validation.
* **Global Exception Handling:** Centralized error management using the new .NET 10 `IExceptionHandler`.
* **Fluent Validation:** Clean DTOs with decoupled validation rules for maximum testability.
* **JWT Authentication:** Secure access control for workers and administrators.
* **Unit Testing:** Core business logic covered with xUnit and Moq.

## 🛠 Tech Stack
* **Backend:** ASP.NET Core 10
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Validation:** FluentValidation
* **Mapping:** AutoMapper
* **Architecture:** Repository Pattern & Unit of Work
* **Documentation:** Scalar 
* **Testing:** xUnit, Moq, FluentAssertions

## 🏗 Project Structure

The solution is divided into logical layers to ensure **Separation of Concerns**:

* **`OfficeBooker.API`**: Entry point, Controllers, and Middleware configuration.
* **`OfficeBooker.Services`**: Business logic, Service implementations, and DTO Validators.
* **`OfficeBooker.DataAccess`**: Database Context, Migrations, and Repository implementations.
* **`OfficeBooker.Models`**: Database Entities, Data Transfer Objects (DTOs), and Custom Exceptions.
* **`OfficeBooker.Tests`**: Unit tests for Services and Validation logic.

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* SQL Server (LocalDB or Express)

### Installation
1.**Clone the repository:**
  bash
   git clone [https://github.com/FilipMi/OfficeBooker.git](https://github.com/FilipMi/OfficeBooker.git)

2.**Setup Local Configuration:**
Locate `appsettings.example.json` in the API project.
Create a copy named appsettings.json.
Update the DefaultConnection string with your local SQL Server details and set your Jwt:Key

3.**Apply Migrations:**
Open your terminal in the solution folder and run:
dotnet ef database update --project OfficeBooker.DataAccess --startup-project OfficeBooker.API

4.**Run the API:**
dotnet run --project OfficeBooker.API

## 📖API Documentation
Once the application is running, you can explore and test the endpoints using:

Scalar : https://localhost:[PORT]/scalar/v1

The API returns standardized error responses even for validation failures, thanks to the integrated Global Exception Handler

## 🧪 Testing
To run the unit tests and verify the business logic:
dotnet test
 
 ---

## 🚀 Roadmap / Future Enhancements

While the core functionality is solid, I plan to expand the system with the following features:

- [ ] **Add more advanced CRUD:** Implement update reservations method.
- [ ] **Email Notifications:** Integrate SendGrid to notify workers about successful bookings or upcoming reservation changes.
- [ ] **Admin Dashboard:** Add a visual dashboard with statistics on office occupancy and peak booking hours.
- [ ] **Advanced Filtering:** Enable users to filter desks by equipment (e.g., "dual monitor", "standing desk").
- [ ] **Calendar Integration:** Sync reservations with Google Calendar or Microsoft Outlook.
- [ ] **Basic UI:** Develop a basic UI using React or Blazor to provide a seamless user experience.

---

## 👤 Author
Filip Mirzejewski

GitHub: https://github.com/FilipMi
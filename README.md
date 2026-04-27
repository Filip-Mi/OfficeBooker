# OfficeBooker API
OfficeBooker API is a backend solution for managing office reservations. Built with .NET 10,it offers a comprehensive suite of features for office space management.
### 🚀 Key Features

* **Office Management:** Full CRUD operations for managing office spaces.
* **Reservation System:** Advanced booking logic with overlapping date validation.
* **Global Exception Handling:** Centralized error management using the new .NET 10 `IExceptionHandler`.
* **Fluent Validation:** Clean DTOs with decoupled validation rules for maximum testability.
* **JWT Authentication:** Secure access control for workers and administrators.


### 🛠 Tech Stack
* **Backend:** ASP.NET Core 10.
* **Database:** SQL Server.
* **ORM:** Entity Framework Core.
* **Validation:** FluentValidation.
* **Mapping:** AutoMapper.
* **Architecture:** Repository Pattern & Unit of Work.
* **Documentation:** Scalar .
* **Testing:** xUnit, Moq, FluentAssertions.

### 🏗 Project Structure

The solution is divided into logical layers to ensure **Separation of Concerns**:

* **`OfficeBooker.API`**: Entry point, Controllers, and Middleware configuration.
* **`OfficeBooker.Services`**: Business logic, Service implementations, and DTO Validators.
* **`OfficeBooker.DataAccess`**: Database Context, Migrations, and Repository implementations.
* **`OfficeBooker.Models`**: Database Entities, Data Transfer Objects (DTOs), and Custom Exceptions.
* **`OfficeBooker.Tests`**: Unit tests for Services.(Currently Under Construction)

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* SQL Server
* EF Core Tools (`dotnet tool install --global dotnet-ef`)

### Installation
1.**Clone the repository:**
  bash
   git clone [https://github.com/Filip-Mi/OfficeBooker.git](https://github.com/Filip-Mi/OfficeBooker.git)

2.**Setup Local Configuration:**
Locate `appsettings.example.json` in the  project.
Create a copy named appsettings.json.
Update the DefaultConnection string with your local SQL Server details and set your Jwt:Key

3.**Apply Migrations:**
Open your terminal in the solution folder and run:
dotnet ef database update --project OfficeBooker.DataAccess --startup-project OfficeBooker

4.**Run the API:**
dotnet run --project OfficeBooker
After running the API, check the console output for the assigned port.

### 📖API Documentation
Once the application is running, you can explore and test the endpoints using:

Scalar : https://localhost:[PORT]/scalar/v1.

* **Descriptions:** Every endpoint includes detailed summaries and parameter descriptions generated from XML comments.
* **Authentication:** Fully supports JWT Bearer authorization directly within the browser.
* **Standardized Errors:** Documentation includes expected error schemas (400, 401, 404) handled by the Global Exception Provider.

## Testing Authenticated Endpoints
1. Use the `/api/Auth/login` endpoint to obtain a JWT token.
2. Click the **"Authorize"** button (or the lock icon) in Scalar.
3. Paste your token.
4. Now you can test protected resources like `/api/Auth/me`.


### 👤 Seeded Test Accounts
The database is pre-configured with the following accounts for testing roles and permissions:

| Role | Email | Password |
| :--- | :--- | :--- |
| **Administrator** | `admin@office.com` | `Admin123!` |
| **Worker** | `user@office.com` | `User123!` |

### 🏢 Sample Offices
The following offices are available in the system by default. You can use their **IDs** (1-4) to test reservations:

| ID | Office | Floor | Capacity | Equipment |
|:---|:---|:---|:---|:---|
| **1** | 101 | 1 | 4 | Monitor, Whiteboard |
| **2** | 102 | 1 | 2 | Dual Monitor |
| **3** | 201 | 2 | 6 | Projector, Conference Mic |
| **4** | 301 | 3 | 1 | Standing Desk |


### 🧪 Testing
To run the unit tests and verify the business logic:
dotnet test
 
 ---

### 🚀 Roadmap / Future Enhancements

While the core functionality is solid, I plan to expand the system with the following features:

- [ ] **Email Notifications:** Integrate SendGrid to notify workers about successful bookings or upcoming reservation changes.
- [ ] **Admin Dashboard:** Add a visual dashboard with statistics on office occupancy and peak booking hours.
- [ ] **Advanced Filtering:** Enable users to filter desks by equipment (e.g., "dual monitor", "standing desk").
- [ ] **Calendar Integration:** Sync reservations with Google Calendar or Microsoft Outlook.
- [ ] **Basic UI:** Develop a basic UI using React or Blazor to provide a seamless user experience.

---

### 👤 Author
Filip Mirzejewski

GitHub: https://github.com/Filip-Mi

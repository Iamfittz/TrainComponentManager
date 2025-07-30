# Train Component Management Application

🚆 A backend ASP.NET Core 8.0 app for managing train components with support for quantity assignment and validation rules.

## 🔧 Technologies Used

- .NET 8
- Entity Framework Core
- MSSQL (LocalDb or full server)
- xUnit (Unit testing)

## 📦 Features

- Create, update, and delete train components
- Validation for quantity assignment (positive integers only)
- Get all components or a specific one by ID
- Unit tests using xUnit

## 🛠️ How to Run Locally

1. **Clone the repository**

   ```bash
   git clone https://github.com/iamfitzz/TrainComponentManager.git
   cd TrainComponentManager
   ```

2. **Make sure MSSQL LocalDB is installed**  
   Or update the connection string in `appsettings.json` to point to your SQL Server instance.

3. **Apply migrations and create the database**
   ```bash
   dotnet ef database update
   ```

4. **Start the application**
   ```bash
   dotnet run --project Train_Management_App
   ```

## 🧪 Running Tests

```bash
dotnet test
```

## 📌 Notes

- The current version uses `InMemoryDatabase` in tests.

# Dog Shelter Management System

A comprehensive .NET Web API for managing a dog shelter with complete CRUD operations for dogs, adopters, adoptions, donations, and veterinary appointments. The project demonstrates advanced testing techniques using **Mocks** and **Stubs** to verify business logic independently from external dependencies.

## Features

- **Dog Management** - Add, update, delete, and retrieve dogs from the shelter
- **Adopter Registration** - Manage potential adopters with detailed profiles
- **Adoption Tracking** - Connect dogs with adopters and track adoption history
- **Donations System** - Process and track donations with validation
- **Veterinary Services** - Schedule and manage veterinary appointments for shelter dogs
- **Mock Services** - Email notifications, logging, and veterinary scheduling using mock implementations
- **In-Memory Storage** - Fast data access using GlobalData for development and testing
- **Comprehensive Testing** - Automated tests using xUnit and Moq

## Testing (Mock & Stub)

The project extensively uses test doubles to isolate and verify business logic without external dependencies.

| Technique | Purpose | Implementation |
|-----------|---------|----------------|
| **Mock** | Simulates dependencies and verifies interactions | `Moq` library for behavior verification |
| **Stub** | Provides predefined responses for isolated testing | Custom stub implementations in `Stubs/` folder |

### Test Coverage

- **DonationServiceTests** - Validates donation processing logic
- **EmailServiceTests** - Verifies email notification behavior
- **LoggerServiceTests** - Ensures proper logging functionality
- **VeterinaryServiceTests** - Tests appointment scheduling
- **IntegrationTests** - End-to-end API testing

Tests validate service logic without requiring real email servers, databases, or external APIs.

## Technologies Used

| Component | Description |
|-----------|-------------|
| **.NET 8.0** | Core framework for Web API |
| **C#** | Primary programming language |
| **Minimal APIs** | Lightweight HTTP API endpoints |
| **xUnit** | Unit testing framework |
| **Moq** | Mocking framework for test doubles |
| **In-Memory Storage** | Fast data persistence using `GlobalData` |
| **Dependency Injection** | Built-in .NET DI container |

## Project Structure

```
DogShelter/
├── APIs/
│   ├── DogsApi.cs              # Dog management endpoints
│   ├── AdoptersApi.cs          # Adopter registration endpoints
│   ├── AdoptionsApi.cs         # Adoption tracking endpoints
│   ├── DonationsApi.cs         # Donation processing endpoints
│   └── VeterinaryApi.cs        # Veterinary appointment endpoints
│
├── Interfaces/
│   ├── IDonationService.cs     # Donation service contract
│   ├── IEmailService.cs        # Email service contract
│   ├── ILoggerService.cs       # Logger service contract
│   └── IVeterinaryService.cs   # Veterinary service contract
│
├── Mocks/
│   ├── DonationServiceMock.cs  # Mock donation implementation
│   ├── EmailServiceMock.cs     # Mock email implementation
│   ├── LoggerServiceMock.cs    # Mock logger implementation
│   └── VeterinaryServiceMock.cs# Mock veterinary implementation
│
├── Stubs/
│   └── StubDataProvider.cs     # Stub data for testing
│
├── Data/
│   └── GlobalData.cs           # In-memory data storage
│
├── Pages/
│   ├── DogsPage.cs             # Dog management UI
│   ├── AdoptersPage.cs         # Adopter management UI
│   ├── AdoptionsPage.cs        # Adoption tracking UI
│   ├── DonationsPage.cs        # Donations UI
│   ├── VeterinaryPage.cs       # Veterinary appointments UI
│   ├── DashboardPage.cs        # Main dashboard
│   ├── MockDemoPage.cs         # Mock demonstration page
│   └── TestMocksPage.cs        # Test interface page
│
├── Tests/
│   └── TestsApi.cs             # Test execution endpoints
│
├── DogShelter.Tests/
│   ├── DonationServiceTests.cs # Donation service tests
│   ├── EmailServiceTests.cs    # Email service tests
│   ├── LoggerServiceTests.cs   # Logger service tests
│   ├── VeterinaryServiceTests.cs # Veterinary service tests
│   └── IntegrationTests.cs     # Integration tests
│
├── Program.cs                  # Application entry point
└── DogShelter.csproj           # Project configuration
```

## Run Instructions

### 1. Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 / VS Code / Rider (optional)

### 2. Clone the Repository
```bash
git clone https://github.com/madaiancu/proiect-tas-2.git
cd "proiect tas 2"
```

### 3. Restore Dependencies
```bash
dotnet restore
```

### 4. Build the Project
```bash
dotnet build
```

### 5. Run the Application
```bash
dotnet run
```

The API will start at: **http://localhost:9000**

### 6. Run Tests
```bash
cd DogShelter.Tests
dotnet test
```

## API Endpoints

### Dogs API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/dogs` | Get all dogs |
| POST | `/api/dogs` | Add new dog (requires JSON body) |

**POST /api/dogs - Request Body:**
```json
{
  "name": "Rex",
  "breed": "Labrador",
  "age": 3,
  "weight": 25.5,
  "health": "Excelentă"
}
```

### Adopters API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/adopters` | Get all registered adopters |
| POST | `/api/adopters` | Register new adopter (requires JSON body) |

**POST /api/adopters - Request Body:**
```json
{
  "name": "Ion Popescu",
  "email": "ion@example.com",
  "phone": "0721234567",
  "age": 35,
  "experience": "Am avut câini înainte",
  "housing": "Casă cu curte",
  "motivation": "Doresc să adopt un câine"
}
```

### Adoptions API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/adoptions` | Get all adoptions |
| POST | `/api/adoptions` | Create new adoption (requires JSON body) |

### Donations API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/donations` | Get all donations |
| POST | `/api/donations` | Process new donation (requires JSON body) |

### Veterinary API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/veterinary/appointments` | Get all appointments |
| POST | `/api/veterinary/appointments` | Schedule new appointment |

### Tests API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/tests/*` | Execute and view test results |

## Configuration

The application uses in-memory storage with pre-populated sample data:
- 3 dogs (Rex, Bella, Max)
- 2 adopters (Ion Popescu, Maria Ionescu)
- Empty collections for adoptions, donations, and veterinary appointments

Data is stored in `Data/GlobalData.cs` and persists during the application runtime.

## Mock Services Integration

The application demonstrates dependency injection with mock services:

```csharp
builder.Services.AddSingleton<IEmailService, EmailServiceMock>();
builder.Services.AddSingleton<IVeterinaryService, VeterinaryServiceMock>();
builder.Services.AddSingleton<IDonationService, DonationServiceMock>();
builder.Services.AddSingleton<ILoggerService, LoggerServiceMock>();
```

This architecture allows easy swapping between mock and real implementations without changing business logic.

## Example Usage

### Adding a Dog
```bash
curl -X POST http://localhost:9000/api/dogs \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Buddy",
    "breed": "Beagle",
    "age": 2,
    "weight": 15.0,
    "health": "Bună"
  }'
```

**Response:**
```json
{
  "success": true,
  "dog": {
    "id": 4,
    "name": "Buddy",
    "breed": "Beagle",
    "age": 2,
    "weight": 15.0,
    "health": "Bună",
    "dateAdded": "2025-11-26T..."
  },
  "totalDogs": 4,
  "mockServices": {
    "logger": "Acțiune înregistrată",
    "veterinary": "Programare creată pentru 03.12.2025",
    "email": "Notificare trimisă la admin"
  }
}
```

## Learning Objectives

This project demonstrates:
- RESTful API design with Minimal APIs
- Dependency Injection and Inversion of Control
- Mock objects for unit testing
- Stub data providers for integration testing
- Separation of concerns (APIs, Services, Data)
- Interface-based programming
- Test-driven development practices

## Author

**Mădălina-Maria Iancu**  
GitHub: [@madaiancu](https://github.com/madaiancu16)

## License

This project is part of the TAS (Testarea Aplicațiilor Software) course assignment.

---

**Note:** This is an educational project demonstrating testing techniques with Mocks and Stubs. In a production environment, real implementations of email services, databases, and external APIs would replace the mock implementations.

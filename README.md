# 🚀 Web API Learning Repository

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![ASP.NET](https://img.shields.io/badge/ASP.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)

A comprehensive learning repository for mastering ASP.NET Core Web API development. This project demonstrates practical implementations of essential Web API concepts including JWT authentication, multiple database contexts, AutoMapper, action filters, Serilog logging, image upload, and consuming APIs with an MVC client application.

---


## 🎯 About

This repository serves as a practical learning resource for building production-ready ASP.NET Core Web APIs. It covers fundamental concepts and advanced implementations that you'll encounter in real-world applications.

### What's Implemented

This project demonstrates a complete Web API solution with:
- RESTful API design and best practices
- Secure authentication and authorization using JWT
- Multiple database contexts in a single application
- Action filters for cross-cutting concerns
- Object mapping with AutoMapper
- Repository pattern for data access
- Structured logging with Serilog
- File upload functionality
- API consumption from an MVC client application using HttpClient

---

## ✨ Implemented Features

### Authentication & Authorization
- **ASP.NET Core Identity** - User registration and management
- **JWT Token Authentication** - Secure token-based authentication
- **Role-Based Authorization** - Admin, User roles implementation
- **Claims-Based Authorization** - Custom claims and policies
- Token generation with configurable expiration
- Refresh token support
- Secured API endpoints with `[Authorize]` attribute

### Data Access & Persistence
- **Entity Framework Core** - ORM for database operations
- **Two Database Contexts** - Managing multiple databases in one application
  - Primary database for main application data
  - Secondary database for specific modules/features
- **Repository Pattern** - Generic repository implementation

### Object Mapping
- **AutoMapper** - Automatic object-to-object mapping
- Custom mapping profiles for entities and DTOs
- DTO (Data Transfer Objects) pattern implementation
- Projection and flattening support
- Prevents over-posting and under-posting

### Action Filters
- **Custom Action Filters** for cross-cutting concerns:
  - Validation filters for model state validation
  - Logging filters for request/response logging
  - Authorization filters for custom authorization logic
  - Exception filters for global error handling
  - Performance monitoring filters
- Filter order and execution pipeline understanding

### Logging
- **Serilog** - Structured logging implementation
- Multiple sinks configuration:
  - File logging with rolling policies
  - Console logging for development
- Log levels (Information, Warning, Error)
- Contextual logging with structured data
- Request/response logging middleware
- Exception logging with stack traces

### File Management
- **Image Upload** functionality
- File validation (type, size, extension)
- Secure file storage
- Serving static files
- File naming conventions to prevent conflicts
- Support for multiple file formats (JPEG, PNG)

### API Consumption
- **HttpClient** implementation with best practices
- **HttpClientFactory** for efficient client management
- **MVC Client Application** consuming the Web API
- Typed HTTP clients
- Service layer for API communication
- Error handling and retry policies
- Deserializing JSON responses
- Sending authenticated requests with JWT tokens

### API Documentation
- **Swagger/OpenAPI** integration
- Interactive API documentation
- Try-it-out functionality
- Request/response examples
- Authentication configuration in Swagger UI

---

## 🛠️ Technologies & Packages

### Core Technologies
- **.NET 9.0** (or your version)
- **C# 13**
- **ASP.NET Core Web API**
- **ASP.NET Core MVC**
- **Entity Framework Core**

### NuGet Packages

#### Authentication & Security
```xml
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" />
```

#### Data Access
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" />
```

#### Mapping
```xml
<PackageReference Include="AutoMapper" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" />
```

#### Logging
```xml
<PackageReference Include="Serilog.AspNetCore" />
<PackageReference Include="Serilog.Sinks.Console" />
<PackageReference Include="Serilog.Sinks.File" />
```

#### API Documentation
```xml
<PackageReference Include="Swashbuckle.AspNetCore" />
```

#### HTTP Communication
```xml
<PackageReference Include="Microsoft.Extensions.Http" />
```



## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- SQL Server (LocalDB, Express, or Full)
- Visual Studio 2026 / VS Code / JetBrains Rider
- Postman or similar API testing tool (optional)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/MahmoudNofaal/Web_API_Learning.git
   cd Web_API_Learning
   ```


3. **Apply Database Migrations**
   ```bash
   # Navigate to the API project directory
   cd MyApplication
   
   # Apply migrations for the first database
   dotnet ef database update --context ApplicationDbContext
   
   # Apply migrations for the second database
   dotnet ef database update --context ApplicationAuthDbContext
   ```


## MVC Client

The MVC client application demonstrates consuming the Web API with:

### Features
- User-friendly interface for API interaction
- CRUD operations on products
- Error handling and validation
- Responsive design

### Implementation Highlights
- **HttpClientFactory** for efficient HTTP communication
- **Typed clients** for each API resource
- **Token management** for authenticated requests
- **Model validation** on client side
- **Error handling** with user-friendly messages

---

## Learning Resources

### Topics Covered in This Repository

1. **Web API Fundamentals**
   - RESTful API design
   - HTTP verbs and status codes
   - Routing strategies
   - Model binding and validation

2. **Authentication & Authorization**
   - ASP.NET Core Identity
   - JWT implementation
   - Role-based access control
   - Claims and policies

3. **Data Access Patterns**
   - Entity Framework Core
   - Repository pattern
   - Multiple DbContexts

4. **Cross-Cutting Concerns**
   - Action filters
   - Custom middleware
   - Exception handling
   - Logging strategies

5. **API Best Practices**
   - DTOs and AutoMapper
   - Async/await patterns
   - API versioning considerations
   - Documentation with Swagger

6. **Client Integration**
   - HttpClient best practices
   - API consumption patterns
   - Error handling
   - Token-based authentication

---

## 🤝 Contributing

This is a learning repository, and contributions are welcome! Feel free to:

- Add new features or examples
- Improve existing implementations
- Fix bugs or issues
- Enhance documentation
- Share learning resources


## 📝 License

This project is open source and available for learning purposes.

---

## 👤 Author

**Mahmoud Nofaal**

- GitHub: [@MahmoudNofaal](https://github.com/MahmoudNofaal)

---

## ⭐ Show Your Support

If you found this repository helpful for learning Web API development, please give it a ⭐️!

---

## 📧 Contact

For questions or suggestions, feel free to open an issue or reach out!

---

**Happy Learning!**
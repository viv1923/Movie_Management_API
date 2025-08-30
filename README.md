<img width="1896" height="734" alt="Screenshot (9)" src="https://github.com/user-attachments/assets/2c777af7-043a-4454-80b0-dd8b4832b83f" />

A .NET 8 Web API project for managing movies with SQL Server as the database.  
This project demonstrates CRUD operations, pagination, unit testing, and stored procedures.

---

## Tech Stack Used
- **Backend Framework**: .NET 8 (ASP.NET Core Web API)  
- **Database**: Microsoft SQL Server (T-SQL + Stored Procedures)  
- **ORM/Data Access**: ADO.NET (not EF Core)  
- **Testing Frameworks**: xUnit, Moq  
- **API Testing Tool**: Swagger (OpenAPI UI)  

---

## Database Objects

**Table**
- GenMovies  

**Stored Procedures**
- sp_InsertMovie  
- sp_UpdateMovie  
- sp_DeleteMovie  

**Queries**
```sql
SELECT * FROM GenMovies;
SELECT * FROM GenMovies WHERE nMovieId = @nMovieId;
```

Project Structure
<img width="1310" height="839" alt="Screenshot (12)" src="https://github.com/user-attachments/assets/02c51c70-a313-48bd-a0fb-dfd1b06f81ed" />

 
 ## Run the Project

### 1 Clone Repository
```bash
git clone https://github.com/your-username/Movie_Management_API.git
cd Movie_Management_API
```

### 2 Configure Database
  - Open appsettings.json
   ```   Update the SQL Server connection string:
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER;Database=MoviesDB;Trusted_Connection=True;TrustServerCertificate=True;"
    }
```
- 3 Run the Project

## The API will start on LocalHost

## Swagger API Documentation
  - Once the project is running, Swagger UI is available at:
  ```https://localhost:portnumber/swagger```

<img width="1477" height="408" alt="Screenshot (8)" src="https://github.com/user-attachments/assets/35d062bb-e371-40cc-998d-a3ab0e247cd0" />

## Pagination Example
```GET /api/Movie/GetAllMovies?pageNumber=2&pageSize=5```

## Example Tests Implemented for:
  - Get all movies returns list 
  - Get movie by ID 
  - Add movie 
  - Update movie 
  - Update parital fields of Movies
  - Delete movie 
  - Pagination logic 

 ## Features Completed
 - CRUD operations with Stored Procedures
 - Pagination in GetAllMovies
 - Swagger API documentation
 - Unit testing with xUnit + Moq
 - SQL Scripts included

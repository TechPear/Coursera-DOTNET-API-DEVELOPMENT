# **UserManagementAPI**

## **Overview**
The **UserManagementAPI** is a simple ASP.NET Core Web API developed as part of the Coursera *Microsoft Copilot for .NET Developers* specialization. The project demonstrates foundational API development skills, including routing, controllers, dependency injection, CRUD operations, and OpenAPI documentation.

The API manages a collection of users stored in an in‑memory data structure and exposes endpoints for creating, retrieving, updating, and deleting user records.

---

## **Features**
- Retrieve all users or a specific user by ID  
- Create new users (server‑generated IDs)  
- Update existing users with partial updates  
- Delete users by ID  
- In‑memory data persistence for simplicity  
- Swagger/OpenAPI documentation for testing and exploration  

---

## **Technologies Used**
- **.NET 8 / ASP.NET Core Web API**
- **C#**
- **Swagger / Swashbuckle**
- **Dependency Injection**
- **In‑memory data storage**

---

## **API Endpoints**

### **GET /api/users**
Returns a list of all users.

### **GET /api/users/{id}**
Returns a specific user by ID.

### **POST /api/users**
Creates a new user.  
The server assigns the ID automatically.

**Example request body:**
```json
{
  "name": "Alice",
  "email": "alice@example.com"
}
```

### **PUT /api/users/{id}**
Updates an existing user.  
Only provided fields are updated (partial update supported).

**Example request body (partial update):**
```json
{
  "email": "newemail@example.com"
}
```

### **DELETE /api/users/{id}**
Deletes a user by ID.

---

## **Project Structure**
```
UserManagementAPI/
│
├── Controllers/
│   └── UsersController.cs
│
├── Models/
│   └── User.cs
│
├── Services/
│   └── UserService.cs
│
├── Program.cs
└── UserManagementAPI.csproj
```

---

## **How to Run the Project**

### **1. Restore dependencies**
```
dotnet restore
```

### **2. Run the API**
```
dotnet run
```

### **3. Open Swagger UI**
Navigate to:
```
https://localhost:<port>/swagger
```

---

# **Use of Microsoft Copilot in the Development Process**

This project was developed as part of the Coursera *Microsoft Copilot for .NET Developers* specialization. Throughout the implementation of the **UserManagementAPI**, Microsoft Copilot assisted in several academically relevant ways that contributed to the quality, structure, and clarity of the final solution.

## **1. Project Setup and Configuration**
Copilot provided guidance on configuring the initial ASP.NET Core Web API project, including middleware setup, Swagger integration, and CORS configuration. This ensured the project followed current .NET conventions and adhered to recommended architectural patterns.

## **2. Code Generation for CRUD Endpoints**
Copilot assisted in generating the initial structure for the CRUD operations used to manage user data. This included controller scaffolding, routing attributes, and method signatures aligned with RESTful API design principles. The generated code served as a foundation that was later refined to meet the project requirements.

## **3. Refinement of Business Logic**
Copilot contributed to improving the logic within the service layer, particularly in areas such as ID handling, partial updates, and preventing unintended modifications. These refinements helped ensure that the API behaved predictably and safely.

## **4. Debugging and Dependency Resolution**
During development, Copilot assisted in identifying and resolving issues related to conflicting OpenAPI and Swagger dependencies. This support helped restore proper functionality to the documentation pipeline and ensured that the API could be tested through Swagger UI.

## **5. Conceptual Clarification**
Copilot provided explanations of key concepts such as CORS, dependency injection, and OpenAPI documentation. These explanations supported a deeper understanding of the underlying technologies and informed decisions made during implementation.

## **6. Documentation and Readability Improvements**
Copilot helped structure and refine documentation sections, including summaries of API behavior and descriptions of architectural decisions. This contributed to a clearer and more academically appropriate presentation of the project.

---

# **Reflection**
Developing this API with the assistance of Microsoft Copilot provided valuable insight into how AI tools can support software development. Copilot accelerated routine tasks, offered explanations that reinforced conceptual understanding, and helped resolve issues efficiently. At the same time, the project required active decision‑making, validation, and refinement, demonstrating the importance of human oversight in AI‑assisted development.


# Version History
v1 – Initial Baseline Implementation
This version represents the foundational structure of the UserManagementAPI created during the early stages of the Coursera specialization. It includes the core components required to demonstrate basic CRUD functionality in an ASP.NET Core Web API.
🧱 Core Features
- Basic project scaffolding using ASP.NET Core Web API.
- Initial User model with Id, Name, and Email fields.
- In‑memory data storage using a simple List<User>.
- CRUD endpoints implemented in UsersController:
- GET /users – retrieve all users
- GET /users/{id} – retrieve a user by ID
- POST /users – create a new user
- PUT /users/{id} – update an existing user
- DELETE /users/{id} – delete a user
- Basic Swagger/OpenAPI documentation enabled for testing.
⚠️ Known Limitations in v1
- No input validation (empty names, invalid emails allowed).
- No duplicate email prevention.
- Missing error handling for invalid IDs or unexpected failures.
- PUT required all fields instead of supporting partial updates.
- API could crash due to unhandled exceptions.
- No performance considerations for data access.
This version served as the baseline for further improvements introduced in v2.



v2 – Enhanced Validation, Stability, and Performance
This version introduces significant improvements to the API’s reliability, correctness, and maintainability. The updates focus on input validation, exception handling, duplicate‑email prevention, and performance optimizations.
🔒 Input Validation Improvements
- Added data annotation attributes (Required, MinLength, EmailAddress) to enforce valid user input.
- Enabled automatic model validation through ASP.NET Core’s model binding pipeline.
- Ensured invalid requests return 400 Bad Request with detailed validation messages.
- Prevented creation or update of users with empty names or invalid email formats.
⚠️ Exception Handling Enhancements
- Wrapped all controller actions in structured try–catch blocks.
- Ensured unexpected errors return a consistent 500 Internal Server Error response.
- Added meaningful error messages for:
- Missing users (404 Not Found)
- Duplicate emails (409 Conflict)
- Invalid input (400 Bad Request)
- Eliminated unhandled exceptions that previously caused API crashes.
📧 Duplicate Email Prevention
- Implemented a centralized email‑uniqueness check in the service layer.
- Prevented creation of users with an email already registered.
- Prevented updates that would result in duplicate emails.
- Returned 409 Conflict when a duplicate is detected.
🚀 Performance Optimizations
- Improved lookup logic using efficient LINQ methods (Any, FirstOrDefault).
- Returned a read‑only view of the user list to avoid unnecessary memory allocations.
- Reduced redundant operations inside update and validation logic.
- Ensured the in‑memory data structure performs efficiently even as the dataset grows.
🧹 Code Quality and Maintainability
- Centralized business rules inside the service layer.
- Simplified controller logic by delegating validation and uniqueness checks.
- Improved readability and consistency across all endpoints.
- Strengthened API behavior to align with real‑world expectations.

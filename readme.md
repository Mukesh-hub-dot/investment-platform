#  Investment Platform - Monorepo

This repository is a **monorepo** containing both the **Frontend (React.js)** and **Backend (ASP.NET Core)** applications for an Investment Portfolio Calculator.

---

##  Monorepo Structure

```bash
investment-platform/
├── backend/                 # ASP.NET Core Web API
│   ├── Investment.API/
│   ├── Investment.Application/
│   ├── Investment.Domain/
│   ├── Investment.Infrastructure/
│   └── ...
├── frontend/                # React.js + Custom CSS
│   ├── public/
│   ├── src/
│   │   ├── components/
│   │   ├── App.js
│   │   └── ...
│   └── ...
└── README.md
```

---

##  Features

-  Calculate ROI based on various investment types and their weightage
-  Real-time validation and remaining percentage calculation
-  Converts AUD ROI into USD using live exchange rate
-  Strategy Pattern in backend for flexible ROI logic
-  Frontend validations for unselected investments and max limits
-  Adheres to **SOLID principles** for scalable and maintainable code

---

##  Setup Instructions

###  Backend (.NET Core)

1. Navigate to the backend folder:
   ```bash
   cd backend/Investment.API
   ```

2. Restore and run the API:
   ```bash
   dotnet restore
   dotnet run
   ```

3. API will be available at:
   ```
   http://localhost:5092/api/investment/calculate
   ```

###  Frontend (React.js)

1. Navigate to the frontend folder:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Run the development server:
   ```bash
   npm start
   ```

4. React app runs at:
   ```
   http://localhost:3000
   ```


##  Technologies Used

### Backend

- ASP.NET Core Web API
- FluentValidation
- Strategy Design Pattern
- SOLID Architecture
- Swagger (for API testing)

### Frontend

- React.js (CRA)
- Functional Components + Hooks
- Custom CSS for styling
- Tab-based layout with controlled navigation

---

##  SOLID Principles Followed

- **S**ingle Responsibility: Each class and component has a focused responsibility.
- **O**pen/Closed: Strategy pattern allows easy extension for new investment types.
- **L**iskov Substitution: Calculators follow a common interface without breaking logic.
- **I**nterface Segregation: Interfaces are specific and minimal.
- **D**ependency Inversion: High-level services depend on abstractions.

---

##  How This Is a Monorepo

- A **monorepo** is a single Git repository that holds multiple projects.
- In this case, both frontend and backend are managed inside the same repo.
- It improves:
  - Unified versioning
  - Easier testing & CI/CD
  - Simpler onboarding for developers

---

##  API Endpoint (for testing)

- `POST /api/investment/calculate`

```json
{
  "totalAmount": 100000,
  "investments": [
    { "type": "Cash", "percentage": 50 },
    { "type": "Shares", "percentage": 50 }
  ]
}
```

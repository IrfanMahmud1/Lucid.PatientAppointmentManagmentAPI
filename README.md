# 🏥 Patient Appointment Management API

## 📘 Overview
**Patient Appointment Management API** is a RESTful **ASP.NET Core Web API** designed for managing patient appointments in a hospital system.  
It supports patient, doctor, and appointment management, including daily token generation, appointment status updates, and multi-tenant (multi-hospital) handling.

This project follows **Clean Architecture** principles and uses the **Generic Repository Pattern** with **Dependency Injection** for scalability and maintainability.

---

## ✨ Features

### Key Features
- ✅ CRUD operations for Patient, Doctor, and Appointments  
- ✅ Clean Architecture 
- ✅ Repository + Unit of Work Pattern for persistence  
- ✅ SQL Server 2019/2022 for data persistence

### 👨‍⚕️ Patient Management
- Add, update, view, and delete patients.
- Prevent duplicates based on **Name** and **Phone**.

### 🩺 Doctor Management
- Add and view doctors.

### 📅 Appointment Management
- Book appointments with **unique daily token numbers per doctor**.
- Prevent duplicate same-day bookings for the same doctor and patient.
- Filter appointments by **Doctor** and **Date**.
- Update appointment **Status** (`Pending`, `Confirmed`, `Cancelled`).
- Token numbers reset automatically each day.
- Prevent booking for past dates.
- Fully supports **multi-tenant hospitals**.

### ⚙️ Additional Features
- **Global Exception Handling**
- **FluentValidation** for input validation.
- **Swagger / OpenAPI** documentation for easy API exploration.

---

## 🏗️ Architecture

**Project Layers:**
- **Domain** – Entities and core business logic.
- **Application Service** – Business rules and DTO mapping.
- **Infrastructure Data** – Database and repository implementation.
- **API** – Controllers and endpoints.

**Key Design Patterns:**
- ✅ Generic Repository for CRUD operations  
- ✅ Dependency Injection for services and repositories  
- ✅ DTOs for data transfer  
- ✅ Centralized exception handling and validation  

---

## 🧰 Prerequisites

- [.NET 7 SDK or later](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)
- [Git](https://git-scm.com/)

---

## ⚙️ Setup Instructions

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/IrfanMahmud1/Lucid.PatientAppointmentManagmentAPI.git

```

### 2️⃣ Configure the Database Connection
Open appsettings.json and set your SQL Server connection string:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=Lucid.PAMS;Trusted_Connection=True;Trust Server Certificate=True;MultipleActiveResultSets=true"
}
```
### 3️⃣ Apply Migrations
```
dotnet ef database update --project Lucid.PAMS.Infrastructure --startup-project Lucid.PAMS.Api

```

### 4️⃣ Insert Sample Data
```
sql/sample-data.sql

```

### 5️⃣ Run the Application
```
dotnet run

```

## 🚀 Sample API Endpoints
### 🧍‍♂️ Patients


➤ Get All Patients

GET /api/patients

➤ Get Patient by ID

GET /api/patients/{id}

➤ Create Patient

POST /api/patients
```
{
  "name": "Kuddus",
  "phone": "01345268974",
  "age": 30,
  "gender": "Male",
  "address": "Nikunjo"
}
```

➤ Update Patient

PUT /api/patients/{id}
```
{
  "name": "Kuddus Rahman",
  "phone": "01345268974",
  "age": 32,
  "gender": "Male",
  "address": "Nikunjo"
}
```

➤ Delete Patient

DELETE /api/patients/{id}

## 👨‍⚕️ Doctors

➤ Get All Doctors

GET /api/doctors

➤ Get Doctor by ID

GET /api/doctors/{id}

➤ Create Doctor

POST /api/doctors

```
{
  "name": "Dr. Smith",
  "department": "Cardiology",
  "phone": "013987654321",
  "fee": 100
}
```

## 📅 Appointments
➤ Get All Appointments

GET /api/appointments

Supports filtering by doctor and date:

```
GET /api/appointments/filter?DoctorId=<guid>&AppointmentDate=2025-12-01
```

➤ Get Appointment by ID

GET /api/appointments/{id}

➤ Book Appointment

POST /api/appointments

```
{
  "patientId": "GUID",
  "doctorId": "GUID",
  "appointmentDate": "2025-12-01T09:00:00",
  "status": "Pending"
}
```

➤ Update Appointment

PUT /api/appointments/{id}

```
{
  "status": "Confirmed"
}
```

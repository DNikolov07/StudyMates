# 📚 StudyMates

> A web application where students find study groups, join them and plan study sessions together.

![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_MVC-8.0-blue)
![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-red)

---

## 📋 Table of Contents

- [About the Project](#-about-the-project)
- [Technologies Used](#️-technologies-used)
- [Prerequisites](#-prerequisites)
- [Getting Started](#-getting-started)
- [Project Structure](#-project-structure)
- [Database Model](#️-database-model)
- [Features](#-features)
- [Usage](#-usage)
- [Configuration](#️-configuration)
- [Contact](#-contact)

---

## 📖 About the Project

StudyMates helps students who want to learn together. A student creates a study group for a subject, sets how many people can join and
describes what the group will work on. Other students browse the groups, join the ones they like and see the schedule of upcoming study
sessions.

The project was built for the *ASP.NET Fundamentals* course and demonstrates the MVC architecture, Entity Framework Core with SQL Server,
a service layer with Dependency Injection, ASP.NET Core Identity and server-side and client-side validation.

---

## 🛠️ Technologies Used

| Technology               | Version | Purpose                              |
|--------------------------|---------|--------------------------------------|
| ASP.NET Core MVC         | 8.0     | Web framework                        |
| Entity Framework Core    | 8.0     | ORM / database access (Code First)   |
| SQL Server (LocalDB)     | -       | Database                             |
| ASP.NET Core Identity    | 8.0     | Registration, login and logout       |
| Razor Views              | -       | Server-side HTML rendering           |
| Bootstrap                | 5       | Responsive styling                   |
| jQuery Validation        | -       | Client-side form validation          |

---

## ✅ Prerequisites

Make sure you have the following installed before running the project:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (version 17.13 or newer, with the **ASP.NET and web development** workload)
- **SQL Server Express LocalDB** – installed together with Visual Studio's ASP.NET workload
- [Git](https://git-scm.com/)

---

## 🚀 Getting Started

Follow these steps to get the project running locally.

### 1. Clone the repository

```bash
git clone https://github.com/DNikolov07/StudyMates.git
```

### 2. Open the solution

Open `StudyMates/StudyMates.slnx` in Visual Studio. NuGet packages are restored automatically on the first build.

### 3. Create the database

> ⚠️ **This step is required.** Without it the application shows a "database operation failed" page.

Open **Tools → NuGet Package Manager → Package Manager Console**, make sure the **Default project** is `StudyMates`, and run:

```powershell
Update-Database
```

This creates the database, all tables and the seeded subjects.

<details>
<summary>Alternative: .NET CLI</summary>

```bash
cd StudyMates/StudyMates
dotnet tool install --global dotnet-ef --version 8.*
dotnet ef database update
```

</details>

### 4. Run the application

Press **F5** in Visual Studio (or run `dotnet run` from the `StudyMates/StudyMates` folder).

The app will be available at `https://localhost:7257` (or `http://localhost:5046`).

---

## 📁 Project Structure

```
StudyMates/
│
├── StudyMates.slnx
└── StudyMates/
    ├── Areas/Identity/       # Identity UI (Register, Login, Logout)
    ├── Common/               # Validation constants shared by models and view models
    ├── Controllers/          # HomeController, StudyGroupController, SessionController
    ├── Data/                 # ApplicationDbContext, seed data and migrations
    ├── Models/               # Entity models: Subject, StudyGroup, StudySession
    ├── Services/             # Service layer (all database access)
    │   └── Interfaces/       # IGroupsService, ISessionService
    ├── ViewModels/           # View models and form models for the views
    ├── Views/                # Razor views, layout and partial views
    ├── wwwroot/              # Static files (CSS, JS, Bootstrap, jQuery)
    ├── appsettings.json      # Connection string and logging
    └── Program.cs            # Service registration and middleware pipeline
```

Controllers never use the `DbContext` directly. They receive the services through constructor injection, and the services handle all database work.

---

## 🗄️ Database Model

| Entity           | Description                                                              |
|------------------|--------------------------------------------------------------------------|
| **Subject**      | A school subject (Mathematics, Programming, …). Seeded with 6 subjects.  |
| **StudyGroup**   | The main entity. Has a title, description, max members, subject and creator. |
| **StudySession** | A planned meeting of a group with topic, date/time and location.        |
| **IdentityUser** | The built-in ASP.NET Core Identity user.                                 |

**Relationships:**

- `Subject` → `StudyGroup`: one-to-many
- `StudyGroup` → `StudySession`: one-to-many (sessions are deleted with their group)
- `IdentityUser` → `StudyGroup` (creator): one-to-many
- `IdentityUser` ↔ `StudyGroup` (members): many-to-many, stored in the `StudyGroupMembers` table

---

## ✨ Features

- [x] User registration, login and logout (ASP.NET Core Identity)
- [x] Full CRUD for study groups (create, list, details, edit, delete)
- [x] Join and leave study groups, with a limit on the number of members
- [x] Study session schedule for every group (add and delete sessions)
- [x] Only the creator of a group can edit or delete it and manage its sessions
- [x] Server-side and client-side validation of all forms
- [x] Responsive UI with Bootstrap
- [x] Layout, partial views (`_StudyGroupCard`, `_StudyGroupForm`) and sections

### Access rules

| Action                                   | Guest | Logged-in user | Group creator |
|------------------------------------------|:-----:|:--------------:|:-------------:|
| Browse groups, view details and sessions | ✅    | ✅             | ✅            |
| Create a group                           | ❌    | ✅             | ✅            |
| Join / leave a group                     | ❌    | ✅             | ❌ (always a member) |
| Edit / delete a group                    | ❌    | ❌             | ✅            |
| Add / delete sessions                    | ❌    | ❌             | ✅            |

---

## 💻 Usage

```
1. Click "Register" in the top-right corner and create an account.
   (No email confirmation is needed – you are logged in right away.)
2. Open "Study Groups" to see all groups.
3. Click "Create Group", fill in the form and choose a subject.
4. Open a group with "Details":
   - as the creator you can Edit, Delete and manage Sessions;
   - as another user you can Join or Leave the group.
5. Click "Sessions" to see the schedule and add new sessions (creator only).
```

> 💡 **Tip:** To test joining and leaving, register a second user in a private/incognito browser window.

---

## ⚙️ Configuration

The connection string is in `StudyMates/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-StudyMates-1ec0c768-e7c3-462e-a0bd-d7aafeda6318;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

- **No environment variables, passwords or API keys are needed.** LocalDB uses Windows authentication.
- **No test accounts are seeded.** Register a new user on the Register page.
- To use a full SQL Server instance instead of LocalDB, change the connection string, for example:

  ```
  Server=.;Database=StudyMates;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true
  ```

  and run `Update-Database` again.

---

## 📬 Contact

**DNikolov07** – [@DNikolov07](https://github.com/DNikolov07)

Project Link: [https://github.com/DNikolov07/StudyMates](https://github.com/DNikolov07/StudyMates)

---

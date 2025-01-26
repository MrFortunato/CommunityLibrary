# Community Library Management System

## Overview

The Community Library Management System is a full-stack web application that enables users to manage books, loans, and notifications for a community library. Built with **ASP.NET Core** for the backend and **Angular** for the frontend, it provides an intuitive interface for library users and administrators.

Key features include:

- User registration and authentication.
- Browsing and searching books.
- Loan management and tracking.
- Automated email notifications for loan reminders and reservation updates.
- Administrative tools for managing books and users.

---

## Features

### General Features

- **User Registration and Login**

  - Secure authentication using JWT (JSON Web Tokens).
  - Role-based access control (users and administrators).

- **Book Management**

  - View, search, and filter books by title, author, genre, or availability.
  - Add, edit, and delete books (admin only).

- **Loan Management**

  - Borrow and return books.
  - Track current and past loans in the user dashboard.

- **Notifications**

  - Email notifications for loan due dates, reminders, and reservation availability.

- **Admin Panel**

  - Manage users and monitor library activity.
  - View pending loans and overdue books.

---

## Tech Stack

### Frontend

- **Framework**: Angular
- **UI Library**: Angular Material
- **State Management**: NgRx
- **Notifications**: ngx-toastr

### Backend

- **Framework**: ASP.NET Core 9.0
- **Database**: MySql
- **ORM**: Entity Framework Core
- **Email Service**: SMTP with MailKit or similar library

### Deployment

- **Frontend Hosting**: Vercel or Netlify
- **Backend Hosting**: Azure App Service or AWS Elastic Beanstalk
- **Database Hosting**: Azure SQL or Amazon RDS

---

## Installation and Setup

### Prerequisites

- **Node.js** and **npm** installed.
- **.NET SDK 7.0** or higher.
- **SQL Server** or **PostgreSQL** database instance.
- SMTP credentials for email notifications.

### Backend Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/your-repo/community-library-backend.git
   cd community-library-backend
   ```

2. Configure database and SMTP settings in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "YourDatabaseConnectionString"
   },
   "SmtpSettings": {
     "Host": "smtp.mailtrap.io",
     "Port": 587,
     "Username": "your-username",
     "Password": "your-password"
   }
   ```

3. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the backend server:

   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5000`.

### Frontend Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/your-repo/community-library-frontend.git
   cd community-library-frontend
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Configure the API URL in `environment.ts`:

   ```ts
   export const environment = {
     production: false,
     apiUrl: 'http://localhost:5000'
   };
   ```

4. Run the Angular development server:

   ```bash
   ng serve
   ```

   The application will be available at `http://localhost:4200`.

---

## Usage

### User Workflow

1. **Register** an account.
2. **Log in** to browse available books.
3. Request a **loan** for a book.
4. Receive **email notifications** for loan deadlines or reservation availability.

### Admin Workflow

1. **Log in** with admin credentials.
2. Manage books: **add, edit, or delete**.
3. View and manage **user accounts** and loans.
4. Monitor pending and overdue loans.

---

## Key Functionalities

### Automated Email Notifications

- **Loan Reminders**: Sent 2 days before the due date.
- **Reservation Updates**: Notify users when a reserved book becomes available.
- **Overdue Alerts**: Notify users and admins of overdue books.

### Background Tasks

- Implemented using `IHostedService` to check and process reminders daily.

---

## Testing

- **Backend**: Unit tests with xUnit.
- **Frontend**: Component tests with Jasmine/Karma.
- **Integration**: Test REST API endpoints with tools like Postman or Swagger.

---

## Future Enhancements

1. Add support for push notifications (browser and mobile).
2. Implement a recommendation system based on user preferences.
3. Allow users to leave reviews and ratings for books.
4.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

---

## Contributing

We welcome contributions! Please fork the repository, create a new branch for your feature or bug fix, and submit a pull request.

---

## Contact

For questions or suggestions, contact:

- **Name**: Fortunato Cassuendi
- **Email**: fortunatocassuendi@hotmail.com
- **GitHub**: [github.com/your-profile](https://github.com/MrFortunato)

Boa 


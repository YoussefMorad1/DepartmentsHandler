### README

# Department Management System (ASP.NET MVC Project)

## Overview

The Department Management System is a web application designed to manage departmental and employee data within an organization. It provides a user-friendly interface for performing CRUD (Create, Read, Update, Delete) operations on departments and employees, leveraging the ASP.NET MVC framework for clean separation of concerns and efficient data handling.

## Features

- **Department Management:** View, add, update, and delete department details.
- **Employee Management:** View, add, update, and delete employee details.
- **Authentication and Authorization:** Includes registration, login, and password recovery via email and cookies.
- **Responsive UI:** Built using HTML, Bootstrap, and jQuery for a seamless user experience.
- **Robust Back-End:** Utilizes Entity Framework Core (EF Core) with a code-first approach for database management on MS SQL Server.
- **Scalable Architecture:** Implements a three-tier architecture (Presentation Layer, Business Logic Layer, Data Access Layer) for maintainability and scalability.
- **Efficient Data Access:** Employs the Repository and Unit of Work patterns to facilitate scalable, transactional data access.

## Technologies Used

- **Frontend**: HTML, Bootstrap, jQuery
- **Backend**: C#, ASP.NET MVC
- **Database**: MS SQL Server, EF Core
- **Data Access**: LINQ, Repository Pattern

## Installation

1. **Clone the repository**:
   ```bash
   git clone [repository link]
   cd [repository folder]
   ```

2. **Setup the database**:
   - Ensure MS SQL Server is installed and running.
   - Update the connection string in the `appsettings.json` file to match your database configuration.
   - Run the following commands to create and seed the database:
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

3. **Run the application**:
   ```bash
   dotnet run
   ```
   The application will start on `http://localhost:5000`.

## Usage

- **Departments**: 
  - Navigate to the Departments page to view all departments.
  - Use the options to add, update, or delete department records.
- **Employees**:
  - Navigate to the Employees page to view all employees.
  - Use the options to add, update, or delete employee records.

## Project Structure

- **Presentation Layer**: Contains the MVC controllers and views for the application.
- **Business Logic Layer**: Includes the service classes that contain business logic.
- **Data Access Layer**: Consists of the repository classes that interact with the database using EF Core.

## Contribution

Contributions are welcome! Please fork the repository and submit pull requests for any improvements or bug fixes.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Contact

For any inquiries or further information, please contact:

**Youssef Morad**
- **Email**: youssefmohammed747@gmail.com
- **LinkedIn**: [linkedin.com/in/youssef-morad](https://www.linkedin.com/in/youssef-morad)
- **GitHub**: [github.com/YoussefMorad1](https://github.com/YoussefMorad1)

---

Feel free to customize the repository link and any other specific details as necessary.

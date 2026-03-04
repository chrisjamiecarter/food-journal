<div align="center">
    <img src="./_resources/food-journal-logo.png" alt="logo" width="100px" />
    <h1>Food Journal</h1>
</div>

Welcome to the **Food Journal** App!

This is a .NET project designed to demonstrate building a complete full-stack web application with authentication, complex relational data modeling, and modern UI design.

Food Journal is a personal meal tracking application that helps users log their daily food intake, track macronutrients, search historical meals, and generate insightful reports about their eating habits.

The web front end is delivered by a Blazor Web App and utilises Tailwind CSS for styling with a vibrant dark theme color palette.
There is an integrated SQL Server database in the back end with Entity Framework Core for data access.

## Requirements

This application fulfills the following [The C# Academy - Food Journal](https://thecsharpacademy.com/project/41/food-journal) project requirements:

- [x] This is an application to track meals.
- [x] Your data schema should have at least a "Meals" and a "Foods" table, in a many-to-many relationship.
- [x] You should have a MealType enumeration (Breakfast, Lunch, Dinner, Snack), which will be recorded in the meals table.
- [x] Your app should have a vibrant color palette.
- [x] There should be a feature that allows the users to quickly record frequent meals.
- [x] There should be a search functionality (per date, per food, per meal type).
- [x] There should be a report feature with queries such as how many times the user had a certain food per period of time.

## Challenges

This project has the following challenges:

- [x] Add macronutrients categorization.
- [ ] Add a "cheat meal" feature to record how many times the user ate junk food or candies.
- [ ] Add goals such as: drinking 20 liters of water in a week, or avoiding chocolate for 7 days.
- [ ] To enrich your app, try to fetch food data from a free Api.

## Features

- **Blazor Web App**:
  - The web front end has been built with Blazor Web App.
- **Tailwind CSS**:
  - The web UI is styled using Tailwind CSS with a vibrant dark theme color palette featuring cyan, emerald, blue, and purple accent colors.
- **Authentication & Authorization**:
  - Users can register, login, and manage their personal food journal data.
  - All data is scoped to the authenticated user for privacy.
- **Meal Tracking**:
  - Users can log meals across four categories: Breakfast, Lunch, Dinner, and Snack.
  - Each meal can contain multiple foods with detailed nutritional information.
- **Quick Meals**:
  - A feature that allows users to quickly record frequently eaten meals with a single click.
  - Users can create, edit, and delete their quick meal presets.
- **Search Functionality**:
  - Advanced search allows filtering meals by date range, food name, and meal type.
  - Paginated results make it easy to browse through historical data.
- **Reports & Analytics**:
  - **Summary Report**: Shows total meals, foods, and quick meals logged by the user.
  - **Food Frequency Report**: Displays how many times a specific food was consumed over various time periods (Week, Month, Year, All Time, or Custom Range).
- **Macronutrients Tracking**:
  - Each food tracks Calories, Carbs, Protein, and Fat.
  - Daily totals are calculated and displayed on the journal dashboard.
- **Entity Framework Core**:
  - Entity Framework Core is used as the ORM with code-first migrations.
- **SQL Server**:
  - SQL Server is used as the data provider.
- **.NET Aspire**:
  - The application uses .NET Aspire for simplified cloud-native development and orchestration.
- **Responsive Web Design**:
  - A user-friendly web interface has been designed to work on various devices.

## Technologies

- .NET 10 SDK
- ASP.NET Core
- Blazor Web App (Interactive WebAssembly)
- Tailwind CSS
- Entity Framework Core
- Microsoft Identity
- SQL Server
- .NET Aspire

## Project Architecture

This project implements a slim version of the **Clean Architecture** pattern to organize the application into distinct layers with clear separation of concerns.

### Layer Structure

- **FoodJournal.AppHost**: The Aspire App Host project that orchestrates the application services and database.

- **FoodJournal.BlazorApp**: The web front-end project built with Blazor Web App. This project handles the UI layer and user interactions.

- **FoodJournal.Application**: The core business logic layer containing:
  - **Entities**: Domain models (Meal, Food, QuickMeal, ApplicationUser)
  - **Enums**: Value types (MealType, ReportPeriod)
  - **DTOs**: Data transfer objects for API communication
  - **Services**: Business logic implementations
  - **Repositories**: Data access abstractions
  - **Database**: Entity configurations and migrations

- **FoodJournal.Common**: Shared resources and common utilities used across projects.

- **FoodJournal.ServiceDefaults**: Shared Aspire service defaults for consistent configuration.

- **FoodJournal.DatabaseMigrator**: A dedicated project for managing database migrations.

### Data Model

The application uses a many-to-many relationship between Meals and Foods, allowing a single meal to contain multiple foods and each food to be part of multiple meals.

Additional entities include QuickMeal for frequently eaten meal presets and ApplicationUser for identity management.

## Getting Started

> [!NOTE]
> The `InitialCreate` database migration has been created.
>
> On start-up of the **AppHost** application, any required database creation/migrations will be performed automatically by the **DatabaseMigrator** application.

### Prerequisites

- .NET 10 SDK.
- An IDE (code editor) like Visual Studio 2022 or Visual Studio Code.
- SQL Server (LocalDB, Express, or a containerized instance).
- Node.js (for Tailwind CSS build).
- Docker.

### Installation

1. Clone the repository:

   - `git clone https://github.com/chrisjamiecarter/food-journal.git`

2. Navigate to the solution directory:

   - `cd food-journal`

3. Restore the .NET packages:

   - `dotnet restore`

4. Configure the application:

   - You should not need to enter any appsettings or secrets, Aspire will handle this for you.

5. Build the application using the .NET CLI:

   - `dotnet build`

### Running the Application

1. Ensure the Docker application is started.

2. You can run the AppHost project from Visual Studio (set **FoodJournal.AppHost** as the startup project).

OR

2. Run the AppHost application using the .NET CLI:

   - `cd src\FoodJournal.AppHost`
   - `dotnet run`

3. The application will start and open in your default web browser at `https://localhost:7202`.

4. Go to the Register page to create a new account to start tracking your meals!

OR

4. For testing: Go to the Register page and click the Generate Test User with Seed Data (Dev Only) button to create a test account to get you started!

## Usage

### Journal

The Journal page is your daily food diary. Use the date picker to navigate between days and add foods to each meal type (Breakfast, Lunch, Dinner, Snack).

The dashboard shows daily totals for Calories, Carbs, Protein, and Fat.

### Quick Meals

Create frequently eaten meals once and log them with a single click in the future.

### Search

Use the Search page to find historical meals by date range, food name, or meal type.

### Reports

Generate reports to see:
- A summary of all your logged data
- How many times you ate a specific food over a given time period

Please refer to the short YouTube video demonstration below:

[![YouTube Video Demonstration](https://github.com/user-attachments/assets/placeholder)](https://youtube.com/watch?v=placeholder "Food Journal Showcase")

## Database Schema

The database contains the following main tables:

- **Meals**: Stores meal records with date and meal type
- **Foods**: Stores food items with nutritional information
- **MealFoods**: Join table for the many-to-many relationship between Meals and Foods
- **QuickMeals**: Stores user-defined quick meal presets
- **FoodQuickMeals**: Join table for the many-to-many relationship between Foods and QuickMeals
- **AspNetUsers**: User identity data
- **AspNetRoles**: Role definitions
- **AspNetUserRoles**: User-role assignments

## Version

This document applies to the FoodJournal v1.0.0 release version.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

## Contact

For any questions or feedback, please open an issue.

---

**_Happy Food Journaling!_**

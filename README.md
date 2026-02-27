# Car Insurance Quote Application

ASP.NET MVC web application that calculates monthly car insurance quotes using Entity Framework and SQL Server LocalDB.

## Features
- Create, Edit, View, and Delete insuree records
- Automatic quote calculation (no manual quote input)
- Admin page listing all issued quotes
- Full CRUD functionality

## Quote Calculation Rules
- Base price: $50/month
- Age:
  - 18 or under → +$100
  - 19–25 → +$50
  - 26+ → +$25
- Car Year:
  - Before 2000 → +$25
  - After 2015 → +$25
- Porsche → +$25
- Porsche 911 Carrera → +$25 additional
- Speeding Tickets → +$10 per ticket
- DUI → +25% of total
- Full Coverage → +50% of total

## Technologies Used
- ASP.NET MVC
- C#
- Entity Framework
- SQL Server LocalDB
- Razor Views

## How to Run
1. Open the solution in Visual Studio.
2. Build the project.
3. Press F5 to run.
4. Navigate to `/Insuree/Index`.


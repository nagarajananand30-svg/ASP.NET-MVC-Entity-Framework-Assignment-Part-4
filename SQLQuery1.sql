CREATE TABLE Insurees
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    EmailAddress NVARCHAR(100),
    DateOfBirth DATETIME,
    CarYear INT,
    CarMake NVARCHAR(50),
    CarModel NVARCHAR(50),
    SpeedingTickets INT,
    DUI BIT,
    CoverageType BIT,
    Quote DECIMAL(18,2)
)

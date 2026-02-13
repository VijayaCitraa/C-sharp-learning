-- =============================================
-- 1️⃣ Create Employees Table
-- =============================================
IF OBJECT_ID('Employees', 'U') IS NOT NULL
    DROP TABLE Employees;
GO

CREATE TABLE Employees
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    Salary INT NOT NULL,
    EmployeeType NVARCHAR(50) NOT NULL,
    TeamSize INT NULL
);
GO

-- =============================================
-- 2️⃣ Create EmployeeAudit Table
-- =============================================
IF OBJECT_ID('EmployeeAudit', 'U') IS NOT NULL
    DROP TABLE EmployeeAudit;
GO

CREATE TABLE EmployeeAudit
(
    AuditId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT,
    Name NVARCHAR(100),
    DeletedOn DATETIME DEFAULT GETDATE()
);
GO

-- =============================================
-- 3️⃣ Create Stored Procedures
-- =============================================

-- Add Employee
IF OBJECT_ID('sp_AddEmployee', 'P') IS NOT NULL
    DROP PROCEDURE sp_AddEmployee;
GO

CREATE PROCEDURE sp_AddEmployee
    @Name NVARCHAR(100),
    @Dept NVARCHAR(100),
    @Salary INT
AS
BEGIN
    INSERT INTO Employees (Name, Department, Salary, EmployeeType)
    VALUES (@Name, @Dept, @Salary, 'Employee')
END
GO

-- Add Manager
IF OBJECT_ID('sp_AddManager', 'P') IS NOT NULL
    DROP PROCEDURE sp_AddManager;
GO

CREATE PROCEDURE sp_AddManager
    @Name NVARCHAR(100),
    @Dept NVARCHAR(100),
    @Salary INT,
    @TeamSize INT
AS
BEGIN
    INSERT INTO Employees (Name, Department, Salary, EmployeeType, TeamSize)
    VALUES (@Name, @Dept, @Salary, 'Manager', @TeamSize)
END
GO

-- List Employees
IF OBJECT_ID('sp_ListEmployees', 'P') IS NOT NULL
    DROP PROCEDURE sp_ListEmployees;
GO

CREATE PROCEDURE sp_ListEmployees
AS
BEGIN
    SELECT * FROM Employees
END
GO

-- Update Employee
IF OBJECT_ID('sp_UpdateEmployee', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdateEmployee;
GO

CREATE PROCEDURE sp_UpdateEmployee
    @Id INT,
    @Name NVARCHAR(100),
    @Dept NVARCHAR(100),
    @Salary INT,
    @TeamSize INT = NULL
AS
BEGIN
    UPDATE Employees
    SET Name = @Name,
        Department = @Dept,
        Salary = @Salary,
        TeamSize = @TeamSize
    WHERE Id = @Id
END
GO

-- Delete Employee
IF OBJECT_ID('sp_DeleteEmployee', 'P') IS NOT NULL
    DROP PROCEDURE sp_DeleteEmployee;
GO

CREATE PROCEDURE sp_DeleteEmployee
    @Id INT
AS
BEGIN
    DELETE FROM Employees WHERE Id = @Id
END
GO

-- =============================================
-- 4️⃣ Create Trigger for Delete Audit
-- =============================================
IF OBJECT_ID('trg_AfterDeleteEmployee', 'TR') IS NOT NULL
    DROP TRIGGER trg_AfterDeleteEmployee;
GO

CREATE TRIGGER trg_AfterDeleteEmployee
ON Employees
AFTER DELETE
AS
BEGIN
    INSERT INTO EmployeeAudit (EmployeeId, Name)
    SELECT Id, Name FROM deleted
END
GO
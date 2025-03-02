 CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    FatherName NVARCHAR(100) NULL,
    Email NVARCHAR(255) NOT NULL CHECK (Email   LIKE '%@%'), -- Email olmalıdır
    PasswordHash NVARCHAR(500) NOT NULL,
    Address NVARCHAR(255) NULL,
    MobilePhone NVARCHAR(20) NULL CHECK (MobilePhone   LIKE '+994%'), -- "+994" ilə başlasın
    CardNumber NVARCHAR(16) NULL CHECK (LEN(CardNumber) = 16), -- 16 rəqəm olmalıdır
    TableNumber NVARCHAR(50) NULL,
    Birthdate DATE NOT NULL,
    DateOfEmployment DATE NOT NULL,
    DateOfDismissal DATE NULL,
    Note NVARCHAR(500) NULL,
    Gender NVARCHAR(20) NOT NULL, -- Enum üçün string saxlaya bilərik
    UserType NVARCHAR(20) NOT NULL, -- Enum üçün string saxlaya bilərik
    
    -- BaseEntity properties
    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    DeletedBy INT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,
    DeletedDate DATETIME NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
);

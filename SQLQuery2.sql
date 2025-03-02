CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Author NVARCHAR(255) NOT NULL,
    Description NVARCHAR(1000) NULL,
    Price DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    CoverPhoto UNIQUEIDENTIFIER NULL,
    UserId Int NOT NULL,
    ShowOnFirstScreen BIT NULL,
    Language NVARCHAR(50) NOT NULL, -- Enum üçün string formatında saxlanılır
    
    -- BaseEntity properties
    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    DeletedBy INT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,
    DeletedDate DATETIME NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,

    FOREIGN KEY (UserId) REFERENCES Users(Id) -- User cədvəli ilə əlaqələndiririk
);

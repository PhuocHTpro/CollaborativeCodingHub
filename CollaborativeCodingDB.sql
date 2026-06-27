
Create database CollaborativeCodingDB;
USE CollaborativeCodingDB;
GO

CREATE TABLE Users
(
    UserID INT IDENTITY(1,1) PRIMARY KEY,

    Username NVARCHAR(50) NOT NULL UNIQUE,

    Password NVARCHAR(100) NOT NULL,

    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

INSERT INTO Users
(
    Username,
    Password
)
VALUES
(
    'admin',
    '123'
);

-- SELECT * FROM Users;

CREATE TABLE Rooms
(
    RoomID INT IDENTITY(1,1) PRIMARY KEY,

    RoomName NVARCHAR(100) NOT NULL,

    CreatedBy INT,

    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Projects
(
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,

    ProjectName NVARCHAR(100) NOT NULL,

    RoomID NVARCHAR(50) NOT NULL,

    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE ProjectFiles
(
    FileID INT IDENTITY(1,1) PRIMARY KEY,

    ProjectID INT NOT NULL,

    FileName NVARCHAR(100) NOT NULL,

    Content NVARCHAR(MAX) DEFAULT '',

    CreatedAt DATETIME DEFAULT GETDATE()
);

-- DROP TABLE Users;
-- DROP TABLE Projects;
-- DROP TABLE ProjectFiles;
-- DROP TABLE Tasks;
-- DROP TABLE Rooms;

GO
CREATE TABLE Tasks
(
    TaskID INT IDENTITY(1,1) PRIMARY KEY,

    ProjectID INT NOT NULL,

    TaskName NVARCHAR(200) NOT NULL,

    Description NVARCHAR(MAX),

    AssignedTo NVARCHAR(100),

    Status NVARCHAR(50),

    CreatedDate DATETIME DEFAULT GETDATE(),

    FOREIGN KEY(ProjectID)
    REFERENCES Projects(ProjectID)
);
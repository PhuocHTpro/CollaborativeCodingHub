/*==========================================================
    Collaborative Coding Database V2
    Part 1
==========================================================*/

USE master;
GO

IF DB_ID('CollaborativeCodingDB') IS NOT NULL
BEGIN
    ALTER DATABASE CollaborativeCodingDB
    SET SINGLE_USER
    WITH ROLLBACK IMMEDIATE;

    DROP DATABASE CollaborativeCodingDB;
END
GO

CREATE DATABASE CollaborativeCodingDB;
GO

USE CollaborativeCodingDB;
GO

/*==========================================================
    USERS
==========================================================*/

CREATE TABLE Users
(
    UserID INT IDENTITY(1,1) PRIMARY KEY,

    Username NVARCHAR(50) NOT NULL UNIQUE,

    Password NVARCHAR(255) NOT NULL,

    FullName NVARCHAR(100),

    Email NVARCHAR(100),

    IsOnline BIT NOT NULL DEFAULT 0,

    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

/*==========================================================
    ROOMS
==========================================================*/

CREATE TABLE Rooms
(
    RoomID NVARCHAR(20) PRIMARY KEY,

    RoomName NVARCHAR(100) NOT NULL,

    OwnerID INT NOT NULL,

    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Rooms_Users
        FOREIGN KEY(OwnerID)
        REFERENCES Users(UserID)
);
GO

/*==========================================================
    ROOM MEMBERS
==========================================================*/

CREATE TABLE RoomMembers
(
    MemberID INT IDENTITY(1,1) PRIMARY KEY,

    RoomID NVARCHAR(20) NOT NULL,

    UserID INT NOT NULL,

    Role NVARCHAR(20)
        DEFAULT 'Member',

    JoinDate DATETIME
        DEFAULT GETDATE(),

    CONSTRAINT FK_RoomMembers_Room
        FOREIGN KEY(RoomID)
        REFERENCES Rooms(RoomID),

    CONSTRAINT FK_RoomMembers_User
        FOREIGN KEY(UserID)
        REFERENCES Users(UserID),

    CONSTRAINT UQ_Room_User
        UNIQUE(RoomID,UserID)
);
GO

/*==========================================================
    INDEX
==========================================================*/

CREATE INDEX IDX_USERNAME
ON Users(Username);

CREATE INDEX IDX_ROOM
ON RoomMembers(RoomID);

GO

/*==========================================================
    SAMPLE USERS
==========================================================*/

INSERT INTO Users
(
Username,
Password,
FullName,
Email
)

VALUES
('admin','123','Administrator','admin@gmail.com'),

('Phuong','123','Minh Phuong','phuong@gmail.com'),

('UserA','123','User A','a@gmail.com'),

('UserB','123','User B','b@gmail.com');

GO

/*==========================================================
    SAMPLE ROOM
==========================================================*/

INSERT INTO Rooms
(
RoomID,
RoomName,
OwnerID
)

VALUES
(
'ROOM001',
'Demo Room',
1
);

GO

/*==========================================================
    ROOM MEMBERS
==========================================================*/

INSERT INTO RoomMembers
(
RoomID,
UserID,
Role
)

VALUES
('ROOM001',1,'Owner'),

('ROOM001',2,'Member'),

('ROOM001',3,'Member');

GO

/*==========================================================
    TEST
==========================================================*/

SELECT * FROM Users;

SELECT * FROM Rooms;

SELECT * FROM RoomMembers;

GO

/*==========================================================
    PROJECTS
==========================================================*/

CREATE TABLE Projects
(
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,

    RoomID NVARCHAR(20) NOT NULL,

    ProjectName NVARCHAR(100) NOT NULL,

    CreatedBy INT NOT NULL,

    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Project_Room
        FOREIGN KEY(RoomID)
        REFERENCES Rooms(RoomID),

    CONSTRAINT FK_Project_User
        FOREIGN KEY(CreatedBy)
        REFERENCES Users(UserID)
);

GO

/*==========================================================
    PROJECT FILES
==========================================================*/

CREATE TABLE ProjectFiles
(
    FileID INT IDENTITY(1,1) PRIMARY KEY,

    ProjectID INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX),

    CreatedBy INT NOT NULL,

    LastModifiedBy INT NOT NULL,

    CreatedDate DATETIME DEFAULT GETDATE(),

    LastModified DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_File_Project
        FOREIGN KEY(ProjectID)
        REFERENCES Projects(ProjectID),

    CONSTRAINT FK_File_CreateUser
        FOREIGN KEY(CreatedBy)
        REFERENCES Users(UserID),

    CONSTRAINT FK_File_ModifiedUser
        FOREIGN KEY(LastModifiedBy)
        REFERENCES Users(UserID)
);

GO

/*==========================================================
    INDEX
==========================================================*/

CREATE INDEX IDX_PROJECT_ROOM
ON Projects(RoomID);

CREATE INDEX IDX_FILE_PROJECT
ON ProjectFiles(ProjectID);

GO

/*==========================================================
    SAMPLE PROJECT
==========================================================*/

INSERT INTO Projects
(
RoomID,
ProjectName,
CreatedBy
)

VALUES
(
'ROOM001',
'Collaborative Coding',
1
);

GO

/*==========================================================
    SAMPLE FILES
==========================================================*/

INSERT INTO ProjectFiles
(
ProjectID,
FileName,
Content,
CreatedBy,
LastModifiedBy
)

VALUES
(
1,
'Program.cs',
'Console.WriteLine("Hello World");',
1,
1
),

(
1,
'User.cs',
'public class User{}',
1,
1
),

(
1,
'UserService.cs',
'public class UserService{}',
1,
1
);

GO

/*==========================================================
    TEST
==========================================================*/

SELECT * FROM Projects;

SELECT * FROM ProjectFiles;

GO

/*==========================================================
    TASKS
==========================================================*/

CREATE TABLE Tasks
(
    TaskID INT IDENTITY(1,1) PRIMARY KEY,

    ProjectID INT NOT NULL,

    TaskName NVARCHAR(200) NOT NULL,
    AssignedTo INT NULL,

    CreatedBy INT NOT NULL,

    Status NVARCHAR(20) NOT NULL DEFAULT 'TODO',
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Task_Project
        FOREIGN KEY(ProjectID)
        REFERENCES Projects(ProjectID)
        ON DELETE CASCADE,

    CONSTRAINT FK_Task_Assigned
        FOREIGN KEY(AssignedTo)
        REFERENCES Users(UserID),

    CONSTRAINT FK_Task_Created
        FOREIGN KEY(CreatedBy)
        REFERENCES Users(UserID),

    CONSTRAINT CK_Task_Status
        CHECK(Status IN
        (
            'TODO',
            'IN_PROGRESS',
            'DONE'
        ))
);

GO

/*==========================================================
    INDEX
==========================================================*/

CREATE INDEX IDX_TASK_PROJECT
ON Tasks(ProjectID);

CREATE INDEX IDX_TASK_STATUS
ON Tasks(Status);

CREATE INDEX IDX_TASK_ASSIGNED
ON Tasks(AssignedTo);

GO

/*==========================================================
    SAMPLE TASK
==========================================================*/

INSERT INTO Tasks
(
ProjectID,
TaskName,
AssignedTo,
CreatedBy,
Status
)

VALUES
(
1,
'Build Login',
2,
1,
'IN_PROGRESS'
),

(
1,
'Realtime Sync',
3,
1,
'TODO'
),

(
1,
'Task Manager',
2,
1,
'DONE'
);

GO

/*==========================================================
    VIEW
==========================================================*/

CREATE VIEW vw_TaskSummary
AS

SELECT

P.ProjectName,

T.TaskName,

T.Status,

T.AssignedTo AS AssignedUserID,

U.Username

FROM Tasks T

INNER JOIN Projects P

ON T.ProjectID=P.ProjectID

LEFT JOIN Users U

ON T.AssignedTo=U.UserID;

GO

/*==========================================================
    TEST
==========================================================*/

SELECT * FROM Tasks;

SELECT * FROM vw_TaskSummary;



/*==========================================================
    FILE HISTORY (Coding Replay)
==========================================================*/

CREATE TABLE FileHistory
(
    HistoryID INT IDENTITY(1,1) PRIMARY KEY,

    FileID INT NOT NULL,

    VersionNo INT NOT NULL,

    Content NVARCHAR(MAX) NOT NULL,

    EditedBy INT NOT NULL,

    EditedTime DATETIME NOT NULL DEFAULT GETDATE(),

    ChangeSummary NVARCHAR(500),

    CONSTRAINT FK_FileHistory_File
        FOREIGN KEY(FileID)
        REFERENCES ProjectFiles(FileID)
        ON DELETE CASCADE,

    CONSTRAINT FK_FileHistory_User
        FOREIGN KEY(EditedBy)
        REFERENCES Users(UserID)
);

GO

/*==========================================================
    INDEX
==========================================================*/

CREATE INDEX IDX_HISTORY_FILE
ON FileHistory(FileID);

CREATE INDEX IDX_HISTORY_VERSION
ON FileHistory(FileID,VersionNo);

GO

/*==========================================================
    SAMPLE FILE HISTORY
==========================================================*/

INSERT INTO FileHistory
(
FileID,
VersionNo,
Content,
EditedBy,
ChangeSummary
)

VALUES
(
1,
1,
'Console.WriteLine("Hello");',
1,
'Initial Version'
),

(
1,
2,
'Console.WriteLine("Hello World");',
2,
'Added Hello World'
),

(
1,
3,
'Console.WriteLine("Collaborative Coding");',
2,
'Changed Output'
);

GO

/*==========================================================
    VIEW : FILE VERSION
==========================================================*/

CREATE VIEW vw_FileVersions
AS

SELECT

F.FileName,

H.VersionNo,

U.Username,

H.EditedTime,

H.ChangeSummary

FROM FileHistory H

INNER JOIN ProjectFiles F

ON H.FileID=F.FileID

INNER JOIN Users U

ON H.EditedBy=U.UserID;

GO

GO

/*==========================================================
    TEST
==========================================================*/

SELECT * FROM FileHistory;

SELECT * FROM vw_FileVersions;

GO

/*==========================================================
    STORED PROCEDURE : REGISTER
==========================================================*/

CREATE PROCEDURE sp_Register
(
    @Username NVARCHAR(50),
    @Password NVARCHAR(255),
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100)
)
AS
BEGIN

    IF EXISTS
    (
        SELECT *
        FROM Users
        WHERE Username=@Username
    )
    BEGIN
        RAISERROR('Username already exists.',16,1);
        RETURN;
    END

    INSERT INTO Users
    (
        Username,
        Password,
        FullName,
        Email
    )
    VALUES
    (
        @Username,
        @Password,
        @FullName,
        @Email
    );

END
GO

/*==========================================================
    STORED PROCEDURE : LOGIN
==========================================================*/

CREATE PROCEDURE sp_Login
(
    @Username NVARCHAR(50),
    @Password NVARCHAR(255)
)
AS
BEGIN

    SELECT *
    FROM Users
    WHERE Username=@Username
    AND Password=@Password;

END
GO

/*==========================================================
    STORED PROCEDURE : CREATE TASK
==========================================================*/

CREATE PROCEDURE sp_CreateTask
(
    @ProjectID INT,
    @TaskName NVARCHAR(200),
    @AssignedTo INT,
    @CreatedBy INT
)
AS
BEGIN

    INSERT INTO Tasks
    (
        ProjectID,
        TaskName,
        AssignedTo,
        CreatedBy
    )
    VALUES
    (
        @ProjectID,
        @TaskName,
        @AssignedTo,
        @CreatedBy
    );

END
GO

/*==========================================================
    STORED PROCEDURE : SAVE FILE HISTORY
==========================================================*/

CREATE PROCEDURE sp_SaveFileHistory
(
    @FileID INT,
    @Content NVARCHAR(MAX),
    @EditedBy INT,
    @ChangeSummary NVARCHAR(500)
)
AS
BEGIN

    DECLARE @Version INT;

    SELECT
    @Version =
    ISNULL(MAX(VersionNo),0)+1
    FROM FileHistory
    WHERE FileID=@FileID;

    INSERT INTO FileHistory
    (
        FileID,
        VersionNo,
        Content,
        EditedBy,
        ChangeSummary
    )
    VALUES
    (
        @FileID,
        @Version,
        @Content,
        @EditedBy,
        @ChangeSummary
    );

END
GO

/*==========================================================
    TRIGGER : UPDATE LAST MODIFIED
==========================================================*/

CREATE TRIGGER trg_UpdateLastModified
ON ProjectFiles
AFTER UPDATE
AS
BEGIN

    UPDATE ProjectFiles

    SET LastModified=GETDATE()

    WHERE FileID IN
    (
        SELECT FileID
        FROM inserted
    );

END
GO

/*==========================================================
    VIEW : PROJECT DASHBOARD
==========================================================*/

CREATE VIEW vw_ProjectDashboard
AS

SELECT

P.ProjectID,

P.ProjectName,

COUNT(DISTINCT F.FileID) AS TotalFiles,

COUNT(DISTINCT T.TaskID) AS TotalTasks,

SUM(CASE WHEN T.Status='DONE' THEN 1 ELSE 0 END) AS CompletedTasks

FROM Projects P

LEFT JOIN ProjectFiles F

ON P.ProjectID=F.ProjectID

LEFT JOIN Tasks T

ON P.ProjectID=T.ProjectID

GROUP BY

P.ProjectID,

P.ProjectName;

GO

/*==========================================================
    TEST
==========================================================*/

SELECT *
FROM vw_ProjectDashboard;

GO

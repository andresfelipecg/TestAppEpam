-- Crear la base de datos TestAppDb
CREATE DATABASE IF NOT EXISTS TestAppDb;

-- Usar la base de datos TestAppDb
USE TestAppDb;

-- Tabla User
CREATE TABLE Users (
    UserId INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    PRIMARY KEY (UserId)
    
);

-- Tabla Subject
CREATE TABLE Subjects (
    SubjectId INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL,
    Description TEXT,
    PRIMARY KEY (SubjectId)
);

-- Tabla StudyGroup
CREATE TABLE StudyGroups (
    StudyGroupId INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL,
    CreateDate DATETIME NOT NULL,
    SubjectId INT NOT NULL,
    UserId INT NOT NULL,
    PRIMARY KEY (StudyGroupId),
    FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Tabla de asociación StudyGroupUsers para la relación muchos-a-muchos
CREATE TABLE StudyGroupUsers (
    StudyGroupId INT NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (StudyGroupId) REFERENCES StudyGroups(StudyGroupId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    PRIMARY KEY (StudyGroupId, UserId)
);

-- Inserts users
INSERT INTO Users (Name, Email) VALUES ('Juan David', 'juan.david@example.com');
INSERT INTO Users (Name, Email) VALUES ('Andres Cuero', 'andrescuero@gmail.com');
INSERT INTO Users (Name, Email) VALUES ('Miguel', 'miguel@gmail.com');
INSERT INTO Users (Name, Email) VALUES ('Manuel', 'manuel@gmail.com');
INSERT INTO Users (Name, Email) VALUES ('Marcos', 'marcos@gmail.com');
INSERT INTO Users (Name, Email) VALUES ('Marcela', 'marcela@gmail.com');
INSERT INTO Users (Name, Email) VALUES ('Laura', 'laura@gmail.com');

-- Insert subjects Math, Chemistry, Physics
INSERT INTO Subjects (Name, Description) VALUES ('Math', 'This subject covers various mathematical concepts.');
INSERT INTO Subjects (Name, Description) VALUES ('Chemistry', 'This subject covers various Chemistry concepts.');
INSERT INTO Subjects (Name, Description) VALUES ('Physics', 'This subject covers various Physics concepts.');

-- Insert StudyGroup
INSERT INTO StudyGroups (Name, CreateDate, SubjectId, UserId)
VALUES ('Math Study Group', NOW(), 1, 1);

-- Adding a Student to an Existing Study Group:
INSERT INTO StudyGroupUsers (StudyGroupId, UserId) VALUES (1, 2);
INSERT INTO StudyGroupUsers (StudyGroupId, UserId) VALUES (1, 3);

-- Select
SELECT * FROM Users;
SELECT * FROM Subjects;
select * from StudyGroups;
Select * from StudyGroupUsers;

-- DELETE FROM StudyGroups WHERE StudyGroupId = 2;

-- SQL query that  return "all the StudyGroups which have at least an user with 'name' starting on 'M' sorted by 'creation date'" 
SELECT DISTINCT sg.StudyGroupId, sg.Name, sg.CreateDate, sg.SubjectId, sg.UserId
FROM StudyGroups sg
LEFT JOIN StudyGroupUsers sgu ON sg.StudyGroupId = sgu.StudyGroupId
LEFT JOIN Users u ON u.UserId = sgu.UserId
WHERE (u.Name LIKE 'M%' OR sg.UserId IN (SELECT u.UserId FROM Users u WHERE u.Name LIKE 'M%'))
ORDER BY CreateDate;

-- This query will list all users whose names start with 'M' along with their associated study groups, sorted by the creation date of the study groups
SELECT sg.StudyGroupId, sg.Name AS StudyGroupName, sg.CreateDate, sg.SubjectId, sg.UserId AS CreatorId, u.UserId, u.Name AS UserName
FROM StudyGroups sg
JOIN StudyGroupUsers sgu ON sg.StudyGroupId = sgu.StudyGroupId
JOIN Users u ON u.UserId = sgu.UserId
WHERE u.Name LIKE 'M%'
UNION
SELECT sg.StudyGroupId, sg.Name AS StudyGroupName, sg.CreateDate, sg.SubjectId, sg.UserId AS CreatorId, u.UserId, u.Name AS UserName
FROM StudyGroups sg
JOIN Users u ON sg.UserId = u.UserId
WHERE u.Name LIKE 'M%'
ORDER BY CreateDate;

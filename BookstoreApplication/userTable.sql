CREATE DATABASE BookstoreDB;

CREATE TABLE Users (
UserID int primary key identity,
Name varchar(255),
Email varchar(255),
Password varchar(255),
Phone varchar(30)
);

SELECT * FROM dbo.Users;

CREATE PROCEDURE spAddUser
@name varchar(255),
@email varchar(255),
@password varchar(255),
@phone varchar(30)
AS
BEGIN
	INSERT INTO dbo.Users (Name,Email,Password,Phone) VALUES (@name,@email,@password,@phone);
END

DROP PROCEDURE spAddUser

CREATE PROCEDURE spUserLogin
@email varchar(255),
@password varchar(255)
AS
BEGIN
	SELECT Email,Password FROM dbo.Users WHERE Email=@email AND Password=@password;
END

CREATE PROCEDURE spUpdateUser
@userId int,
@name varchar(255),
@email varchar(255),
@password varchar(255),
@phone varchar(30)
AS
BEGIN
	UPDATE dbo.Users SET Name=@name,Email=@email,Password=@password,Phone=@phone WHERE UserID=@userId
END

CREATE PROCEDURE spForgetUser
@email varchar(255)
AS
BEGIN
	SELECT Name FROM dbo.Users WHERE Email=@email
END
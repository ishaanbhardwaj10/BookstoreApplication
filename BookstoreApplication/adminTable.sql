CREATE TABLE Admin (
AdminID int primary key identity,
Name varchar(255),
Email varchar(255),
Password varchar(255),
Phone varchar(30)
);

SELECT * FROM dbo.Admin;

INSERT INTO dbo.Admin (Name,Email,Password,Phone) VALUES ('Ishaan Bhardwaj','ishaan@email.com','Ishaan@1234',8498374323);

UPDATE dbo.Admin SET Password='SXNoYWFuQDEyMzQ=' WHERE AdminID=1;

CREATE PROCEDURE spAdminLogin
@email varchar(255),
@password varchar(255)
AS
BEGIN
	SELECT Email,Password FROM dbo.Admin WHERE Email=@email AND Password=@password;
END
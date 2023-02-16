CREATE TABLE Address (
AddressID INT PRIMARY KEY IDENTITY,
Address VARCHAR(max),
City VARCHAR(255),
State VARCHAR(255),
TypeID INT FOREIGN KEY REFERENCES dbo.AddressType(TypeID),
UserID INT
);
alter table dbo.Address add constraint fk_UserID_Address foreign key(UserID) references dbo.Users(UserID);

CREATE TABLE AddressType (
TypeID INT PRIMARY KEY IDENTITY,
Type VARCHAR(255)
);
INSERT INTO dbo.AddressType (Type) VALUES ('Home'),('Work');
SELECT * FROM dbo.AddressType;

CREATE PROCEDURE spAddAddress
@address VARCHAR(max),
@city VARCHAR(255),
@state VARCHAR(255),
@typeID INT,
@userID INT
AS
BEGIN
	INSERT INTO dbo.Address (Address,City,State,TypeID,UserID) VALUES (@address,@city,@state,@typeID,@userID);
END

CREATE PROCEDURE spUpdateAddress
@addressID INT,
@address VARCHAR(max),
@city VARCHAR(255),
@state VARCHAR(255),
@typeID INT,
@userID INT
AS
BEGIN
	UPDATE dbo.Address SET Address=@address,City=@city,State=@state,TypeID=@typeID WHERE AddressID=@addressID AND UserID=@userID;
END

CREATE PROCEDURE spDeleteAddress
@addressID INT,
@userID INT
AS
BEGIN
	DELETE FROM dbo.Address WHERE AddressID=@addressID AND UserID=@userID;
END

CREATE PROCEDURE spGetAllAddress
@userID INT
AS
BEGIN
	SELECT * FROM dbo.Address WHERE UserID=@userID;
END
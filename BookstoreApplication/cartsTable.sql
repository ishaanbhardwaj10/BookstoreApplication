CREATE TABLE Carts(
CartID INT PRIMARY KEY IDENTITY,
BookID INT FOREIGN KEY REFERENCES dbo.Books(BookID),
UserID INT FOREIGN KEY REFERENCES dbo.Users(UserID),
CartQuantity INT
);

SELECT * FROM dbo.Carts;

CREATE PROCEDURE spAddToCart
@bookId int,
@userId int,
@cartQuantity int
AS
BEGIN
	INSERT INTO dbo.Carts (BookID,UserID,CartQuantity) VALUES (@bookId,@userId,@cartQuantity);
END

ALTER PROCEDURE spUpdateCart
@cartId int,
@userId int,
@cartQuantity int
AS
BEGIN
	UPDATE dbo.Carts SET CartQuantity=@cartQuantity WHERE CartID=@cartId AND UserID=@userId;
END

CREATE PROCEDURE spDeleteCart
@cartId int,
@userId int
AS
BEGIN
	DELETE FROM dbo.Carts WHERE CartID=@cartId AND UserID=@userId;
END
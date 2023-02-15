CREATE TABLE Wishlist(
WishlistID INT PRIMARY KEY IDENTITY,
BookID INT FOREIGN KEY REFERENCES dbo.Books(BookID),
UserID INT FOREIGN KEY REFERENCES dbo.Users(UserID)
);

SELECT * FROM dbo.Wishlist;

CREATE PROCEDURE spAddToWishlist
@bookId int,
@userId int
AS
BEGIN
	INSERT INTO dbo.Wishlist (BookID,UserID) VALUES (@bookId,@userId);
END

CREATE PROCEDURE spDeleteWishlist
@wishlistId int,
@userId int
AS
BEGIN
	DELETE FROM dbo.Wishlist WHERE WishlistID=@wishlistId AND UserID=@userId;
END

CREATE PROCEDURE spGetWishlistByUserId  
@userId int  
AS  
BEGIN  
 SELECT * FROM dbo.Wishlist WHERE UserID=@userId;  
END
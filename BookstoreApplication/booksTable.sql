CREATE TABLE Books(
BookID int primary key identity,
BookName varchar(255),
AuthorName varchar(255),
Ratings decimal(2,1),
NoOfPeopleRated int,
DiscountedPrice int,
OriginalPrice int,
BookDetails varchar(550),
BookImage varchar(255),
BookQuantity int
);

SELECT * FROM dbo.Books;


CREATE PROCEDURE spAddBook
@bookName varchar(255),
@authorName varchar(255),
@ratings decimal(2,1),
@noOfPeopleRated int,
@discountedPrice int,
@originalPrice int,
@bookDetails varchar(550),
@bookImage varchar(255),
@bookQuantity int
AS
BEGIN
	INSERT INTO dbo.Books (BookName,AuthorName,Ratings,NoOfPeopleRated,DiscountedPrice,OriginalPrice,BookDetails,BookImage,BookQuantity) VALUES (@bookName,@authorName,@ratings,@noOfPeopleRated,@discountedPrice,@originalPrice,@bookDetails,@bookImage,@bookQuantity);
END	


CREATE PROCEDURE spUpdateBook
@bookId int,
@bookName varchar(255),
@authorName varchar(255),
@ratings decimal(2,1),
@noOfPeopleRated int,
@discountedPrice int,
@originalPrice int,
@bookDetails varchar(550),
@bookImage varchar(255),
@bookQuantity int
AS
BEGIN
	UPDATE dbo.Books SET BookName=@bookName,AuthorName=@authorName,Ratings=@ratings,NoOfPeopleRated=@noOfPeopleRated,DiscountedPrice=@discountedPrice,OriginalPrice=@originalPrice,BookDetails=@bookDetails,BookImage=@bookImage,BookQuantity=@bookQuantity WHERE BookID=@bookId;
END


CREATE PROCEDURE spDeleteBook
@bookId int
AS
BEGIN
	DELETE FROM dbo.Books WHERE BookID=@bookId;
END

CREATE PROCEDURE spGetAllBooks
AS
BEGIN
	SELECT * FROM dbo.Books;
END

CREATE PROCEDURE spGetBookById
@bookId int
AS
BEGIN
	SELECT * FROM dbo.Books WHERE BookID=@bookId;
END
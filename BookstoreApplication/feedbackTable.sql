CREATE TABLE Feedback (
FeedbackID INT PRIMARY KEY IDENTITY,
Ratings INT,
Comment VARCHAR(550),
UserID INT FOREIGN KEY REFERENCES dbo.Users(UserID),
BookID INT FOREIGN KEY REFERENCES dbo.Books(BookID)
);

SELECT * FROM dbo.Feedback;

CREATE PROCEDURE spAddFeedback
@ratings INT,
@comment VARCHAR(550),
@userID INT,
@bookID INT
AS
BEGIN
	INSERT INTO dbo.Feedback (Ratings,Comment,UserID,BookID) VALUES (@ratings,@comment,@userID,@bookID);
END

CREATE PROCEDURE spGetAllFeedback
@userId INT
AS
BEGIN
	SELECT * FROM dbo.Feedback WHERE UserID=@userId;
END
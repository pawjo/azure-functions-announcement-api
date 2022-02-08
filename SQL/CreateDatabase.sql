CREATE TABLE Announcement
(
    Id int IDENTITY(1, 1) NOT NULL,
    Title nvarchar(100) NOT NULL,
    Content nvarchar(500),
    AnnouncementType int NOT NULL
)
USE NextLevelBJJ;
Declare @id UNIQUEIDENTIFIER;
set @id = NEWID();
Insert Into Students values (@id, 'Admin', 'Greg', 'Styjziak', NEWID(), 0, DATEADD(DAY, -3960, GETDATE()), 'Opole, Poland', 12391239, 'stysiok@email.com', 1, @id, DATEADD(DAY, -30, GETDATE()), 1, @id, DATEADD(DAY, -30, GETDATE()));
Select * from students;
CREATE PROCEDURE [dbo].[spUser_Get]
	@id int
AS
begin
	select Id, FirstName, LastName
	from dbo.[User]
	Where Id = @Id;
end 



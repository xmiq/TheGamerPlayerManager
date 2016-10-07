-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 8th October 2016
-- Description:	Creates a story
-- =============================================
CREATE PROCEDURE [Player].[usp_CreateStory]
	-- Add the parameters for the stored procedure here
	@Name varchar(50),
	@User varchar(20)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_CreateStory: ';

	/* Validation */
	IF @Name IS NULL OR @Name = ''
	BEGIN
		SET @message = @message + '@Name cannot be empty';
		THROW 50000, @message, 1;
	END

	IF (@User IS NULL OR @User = '')
	BEGIN
		SET @message = @message + '@User cannot be both empty';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		INSERT INTO Player.Story([Name], [User])
		VALUES (@Name, @User)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 14th October 2016
-- Description:	Updates a story
-- =============================================
CREATE PROCEDURE [Player].[usp_UpdateStory]
	-- Add the parameters for the stored procedure here
	@ID int,
	@Name varchar(50),
	@User varchar(20)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_UpdateStory: ';

	/* Validation */
	IF @ID IS NULL OR @ID = 0
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Name IS NULL OR @Name = ''
	BEGIN
		SET @message = @message + '@Name cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @User IS NULL OR @User = ''
	BEGIN
		SET @message = @message + '@User cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @User)
	BEGIN
		SET @message = @message + '@User: ' + @User + ' is not a valid ID for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		UPDATE Player.Story
		SET [Name] = @Name, [User] = @User
		WHERE (ID = @ID)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
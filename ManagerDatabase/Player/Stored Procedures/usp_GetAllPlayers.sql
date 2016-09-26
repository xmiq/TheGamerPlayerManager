-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 9th September 2016
-- Description:	Returns all the players
-- =============================================
CREATE PROCEDURE [Player].[usp_GetAllPlayers]
	-- Add the parameters for the stored procedure here
	@Username varchar(20)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetAllPlayers: ';

	/* Validation */
	IF @Username IS NULL OR @Username = ''
	BEGIN
		SET @message = @message + '@Username cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @Username)
	BEGIN
		SET @message = @message + 'User: ' + @Username + ' does not exist in the database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT [ID], [Name], [Surname], [Story]
		FROM [Player].[Player]
		WHERE [User] = @Username
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
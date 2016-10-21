-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 9th September 2016
-- Description:	Returns all the players of a story
-- =============================================
CREATE PROCEDURE [Player].[usp_GetAllStoryPlayers]
	-- Add the parameters for the stored procedure here
	@Username varchar(20),
	@Story int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetAllStoryPlayers: ';

	/* Validation */
	IF @Username IS NULL OR @Username = ''
	BEGIN
		SET @message = @message + '@Username cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Story IS NULL OR @Story = 0
	BEGIN
		SET @message = @message + '@Story cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @Username)
	BEGIN
		SET @message = @message + 'User: ' + @Username + ' does not exist in the database';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[Story] WHERE ID = @Story)
	BEGIN
		SET @message = @message + 'Story: ' + @Story + ' does not exist in the database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT p.[ID], p.[Name], p.[Surname], s.[ID] AS StoryID, s.[Name] AS StoryName
		FROM [Player].[Player] p
		JOIN [Player].[Story] s ON (s.ID = p.Story)
		WHERE s.[User] = @Username AND s.ID = @Story;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
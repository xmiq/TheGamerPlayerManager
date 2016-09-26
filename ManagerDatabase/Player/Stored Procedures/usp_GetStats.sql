-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 13th September 2016
-- Description:	Returns a player's stats for the chapter
-- =============================================
CREATE PROCEDURE [Player].[usp_GetStats]
	-- Add the parameters for the stored procedure here
	@Chapter int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetStats: ';

	/* Validation */
	IF @Chapter IS NULL OR @Chapter = 0
	BEGIN
		SET @message = @message + '@Chapter cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.Chapter WHERE ID = @Chapter)
	BEGIN
		SET @message = @message + '@Chapter: ' + @Chapter + ' is not a valid Chapter for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT [ID], [Level], [EXP], [Age], [Strength], [Vitality], [Constitution], [Dexterity], [Accuracy], [Inteligence], [Wisdom], [Charisma], [Luck]
		FROM [aotxgmr].[Player].[Status]
		WHERE Chapter = @Chapter;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
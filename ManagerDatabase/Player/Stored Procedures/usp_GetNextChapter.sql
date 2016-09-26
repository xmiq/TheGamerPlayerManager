-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 18 September 2016
-- Description:	Gets the next chapter in line
-- =============================================
CREATE PROCEDURE [Player].[usp_GetNextChapter]
	-- Add the parameters for the stored procedure here
	@Player int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetNextChapter: ';

	/* Validation */
	IF @Player IS NULL OR @Player = 0
	BEGIN
		SET @message = @message + '@Player cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[Player] WHERE ID = @Player)
	BEGIN
		SET @message = @message + '@Player: ' + @Player + ' is not a valid player for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT TOP 1 Number + 1 AS Number
		FROM [Player].[Chapter]
		WHERE Player = @Player
		ORDER BY Number DESC
	END TRY
	BEGIN CATCH
		-- Implement Database Logging
	END CATCH
END
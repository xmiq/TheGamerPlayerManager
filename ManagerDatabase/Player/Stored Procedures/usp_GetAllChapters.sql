-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 10th September 2016
-- Description:	Gets the List of Chapters
-- =============================================
CREATE PROCEDURE [Player].[usp_GetAllChapters]
	-- Add the parameters for the stored procedure here
	@Player int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetAllChapters: ';

	/* Validation */
	IF @Player IS NULL OR @Player = 0
	BEGIN
		SET @message = @message + '@Player cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.Player WHERE ID = @Player)
	BEGIN
		SET @message = @message + '@Player: ' + @Player + ' is not a valid ID for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT ID, Number
		FROM Player.Chapter
		WHERE Player = @Player;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
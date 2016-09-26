-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 26th September 2016
-- Description:	Removes a skill stat
-- =============================================
CREATE PROCEDURE [Player].[usp_DeleteSkillStat]
	-- Add the parameters for the stored procedure here
	@ID int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_DeletePlayer: ';

	/* Validation */
	IF @ID IS NULL OR @ID = ''
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[SkillStats] WHERE ID = @ID)
	BEGIN
		SET @message = @message + '@ID: ' + @ID + ' is not a valid skill status for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Delete Data */
		DELETE FROM  Player.SkillStats
		WHERE        ID = @ID
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
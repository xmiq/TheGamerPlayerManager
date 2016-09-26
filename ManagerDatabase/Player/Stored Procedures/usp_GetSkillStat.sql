-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 25th September 2016
-- Description:	Get the skill stat details
-- =============================================
CREATE PROCEDURE Player.usp_GetSkillStat
	@SkillStatID int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetSkillStat: ';

	/* Validation */
	IF @SkillStatID IS NULL OR @SkillStatID = 0
	BEGIN
		SET @message = @message + '@SkillStatID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.SkillStats WHERE ID = @SkillStatID)
	BEGIN
		SET @message = @message + '@SkillStatID: ' + @SkillStatID + ' is not a valid ID for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT [SkillID], [Level], [Chapter], [EXP]
		FROM Player.SkillStats
		WHERE ID = @SkillStatID;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 25th September 2016
-- Description:	Updates a player's skill stat data
-- =============================================
CREATE PROCEDURE [Player].[usp_UpdateSkillStat]
	-- Add the parameters for the stored procedure here
	@ID int,
	@Level int,
	@EXP int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_UpdateSkillStat: ';

	/* Validation */
	IF @ID IS NULL OR @ID = ''
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Level IS NULL OR @Level = 0
	BEGIN
		SET @message = @message + '@Level cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @EXP IS NULL OR @EXP = 0
	BEGIN
		SET @message = @message + '@EXP cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[SkillStatus] WHERE ID = @ID)
	BEGIN
		SET @message = @message + '@ID: ' + @ID + ' is not a valid skill stat id for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Update Data */
		UPDATE       Player.SkillStatus
		SET          [Level] = @Level, [EXP] = @EXP
		WHERE        ID = @ID
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 26th September 2016
-- Description:	Creates a new skill stat
-- =============================================
CREATE PROCEDURE [Player].[usp_CreateSkillStat]
	-- Add the parameters for the stored procedure here
	@SkillID int,
	@Level int,
	@Chapter int,
	@Player int,
	@EXP int = 0
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_CreateSkillStat: ';

	/* Validation */
	IF @SkillID IS NULL OR @SkillID = 0
	BEGIN
		SET @message = @message + '@SkillID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Level IS NULL OR @Level = 0
	BEGIN
		SET @message = @message + '@Level cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Chapter IS NULL OR @Chapter = 0
	BEGIN
		SET @message = @message + '@Chapter cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Player IS NULL OR @Player = 0
	BEGIN
		SET @message = @message + '@Player cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @EXP IS NULL
	BEGIN
		SET @message = @message + '@EXP cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[Skills] WHERE ID = @SkillID)
	BEGIN
		SET @message = @message + '@SkillID: ' + CAST(@SkillID AS varchar) + ' is not a valid skill id for this database';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[Chapter] WHERE ID = @Chapter)
	BEGIN
		SET @message = @message + '@Chapter: ' + CAST(@Chapter AS varchar) + ' is not a valid chapter id for this database';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[Player] WHERE ID = @Player)
	BEGIN
		SET @message = @message + '@Player: ' + CAST(@Player AS varchar) + ' is not a valid player id for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Add Data */
		INSERT INTO [Player].[SkillStats]
		([SkillID], [Level], [Chapter], [Player], [EXP])
		VALUES
		(@SkillID, @Level, @Chapter, @Player, @EXP);
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
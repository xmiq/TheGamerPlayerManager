-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 13th September 2016
-- Description:	Creates a new Chapter and the Stats for it
-- =============================================
CREATE PROCEDURE [Player].[usp_CreateChapter]
	-- Add the parameters for the stored procedure here
	@Player int,
	@Number int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_CreateChapter: '
		  , @CurrentChapter int
		  , @PreviousChapter int;

	DECLARE @ChapterID TABLE (ID int)

	/* Validation */
	IF @Player IS NULL OR @Player = 0
	BEGIN
		SET @message = @message + '@Player cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Number IS NULL OR @Number = 0
	BEGIN
		SET @message = @message + '@Number cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[Player] WHERE ID = @Player)
	BEGIN
		SET @message = @message + '@Player: ' + @Player + ' is not a valid player for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Insert Data */
		INSERT INTO [Player].[Chapter]
		(Player, Number)
		OUTPUT
		inserted.ID INTO @ChapterID
		VALUES
		(@Player, @Number);

		/* Retreive ID */
		SELECT TOP 1 @CurrentChapter = ID
		FROM @ChapterID;

		/* Get Previous Chapter */
		SELECT TOP 1 @PreviousChapter = ID
		FROM [Player].[Chapter]
		WHERE Player = @Player AND Number = @Number - 1

		/* Insert stats with the values of the previous chapter */
		INSERT INTO [Player].[Stats] (Chapter, [Level], [EXP], Age, Strength, Vitality, Constitution, Dexterity, Accuracy, Inteligence, Wisdom, Charisma, Luck)
		SELECT @CurrentChapter, [Level], [EXP], Age, Strength, Vitality, Constitution, Dexterity, Accuracy, Inteligence, Wisdom, Charisma, Luck
		FROM [Player].[Stats]
		WHERE (Chapter = @PreviousChapter)

	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
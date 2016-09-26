-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 13th September 2016
-- Description:	Returns a player's stats for the chapter
-- =============================================
CREATE PROCEDURE [Player].[usp_UpdateStats]
	-- Add the parameters for the stored procedure here
	@ID int,
	@Chapter int,
	@Level int,
	@EXP int,
	@Age int,
	@Strength int,
	@Vitality int,
	@Constitution int,
	@Dexterity int,
	@Accuracy int,
	@Inteligence int,
	@Wisdom int,
	@Charisma int,
	@Luck int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_UpdateStats: ';

	/* Validation */
	IF @ID IS NULL OR @ID = 0
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Chapter IS NULL OR @Chapter = 0
	BEGIN
		SET @message = @message + '@Chapter cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Level IS NULL
	BEGIN
		SET @message = @message + '@Level cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @EXP IS NULL
	BEGIN
		SET @message = @message + '@EXP cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Age IS NULL
	BEGIN
		SET @message = @message + '@Age cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Strength IS NULL
	BEGIN
		SET @message = @message + '@Stregth cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Vitality IS NULL
	BEGIN
		SET @message = @message + '@Vitality cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Constitution IS NULL
	BEGIN
		SET @message = @message + '@Constitution cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Dexterity IS NULL
	BEGIN
		SET @message = @message + '@Dexterity cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Accuracy IS NULL
	BEGIN
		SET @message = @message + '@Accuracy cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Inteligence IS NULL
	BEGIN
		SET @message = @message + '@Inteligence cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Wisdom IS NULL
	BEGIN
		SET @message = @message + '@Wisdom cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Charisma IS NULL
	BEGIN
		SET @message = @message + '@Charisma cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Luck IS NULL
	BEGIN
		SET @message = @message + '@Luck cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.[Stats] WHERE ID = @ID)
	BEGIN
		SET @message = @message + 'Stat ID: ' + @ID + ' is not a valid stat for this database';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.Chapter WHERE ID = @Chapter)
	BEGIN
		SET @message = @message + '@Chapter: ' + @Chapter + ' is not a valid Chapter for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Update Data */
		UPDATE Player.[Stats]
		SET Chapter = @Chapter, [Level] = @Level, [EXP] = @EXP, Age = @Age, Strength = @Strength, Vitality = @Vitality, Constitution = @Constitution, Dexterity = @Dexterity, Accuracy = @Accuracy, Inteligence = @Inteligence, Wisdom = @Wisdom, Charisma = @Charisma, Luck = @Luck
		WHERE (ID = @ID)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
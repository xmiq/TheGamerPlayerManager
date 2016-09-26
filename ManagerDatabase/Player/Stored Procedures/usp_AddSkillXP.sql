-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 19th September 2016
-- Description:	Increments a skill's XP by the given amount.
-- =============================================
CREATE PROCEDURE [Player].[usp_AddSkillXP]
	@ID int,
	@XPtoAdd int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_AddSkillXP: '
	      , @XP int
		  , @Level int
		  , @MaxXP int
		  , @TotalXP int
		  , @LevelUP bit = 1;

    /* Validation */

	IF @ID IS NULL OR @ID = 0
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @XPtoAdd IS NULL OR @XPtoAdd = 0
	BEGIN
		SET @message = @message + '@XPtoAdd cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.[SkillStatus] WHERE ID = @ID)
	BEGIN
		SET @message = @message + 'There is no Skill Stats for the ID ' + @ID + ' in the database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY

		/* Get Existing XP and Level */
		SELECT @XP = [EXP], @Level = [Level] FROM Player.[SkillStatus] WHERE ID = @ID;

		/* Add XP */
		SET @TotalXP = @XP + @XPtoAdd;

		WHILE @LevelUP = 1
		BEGIN

			/* Calculate Max XP */
			SET @MaxXP = ((@Level /10) + 1) * (@Level * (@Level % 10) * ((@Level * 10) - @Level)) * POWER(@Level, 0.5);

			/* Account for Level 1 weirdness */
			IF @Level = 1
			BEGIN
				SET @MaxXP = @MaxXP * 10;
			END

			/* Check if XP is greater than Max XP */
			IF @TotalXP > @MaxXP
			BEGIN
				/* Level UP */
				SET @Level = @Level + 1;

				/* Remove XP Required to Level UP */
				SET @TotalXP = @TotalXP - @MaxXP
			END
			ELSE
			BEGIN
				/* Break Out of Loop */
				SET @LevelUP = 0;
			END
		END

		/* Save Data */
		UPDATE Player.[SkillStatus]
		SET [EXP] = @TotalXP, [Level] = @Level
		WHERE ID = @ID;

	END TRY
	BEGIN CATCH
		--Do Call Logging
	END CATCH
END
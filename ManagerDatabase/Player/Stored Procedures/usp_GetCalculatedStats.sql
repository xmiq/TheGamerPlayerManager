-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 13th September 2016
-- Description:	Returns a player's calculated stats for the chapter
-- =============================================
CREATE PROCEDURE [Player].[usp_GetCalculatedStats]
	-- Add the parameters for the stored procedure here
	@ID int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetCalculatedStats: ';

	/* Validation */
	IF @ID IS NULL OR @ID = 0
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.[Stats] WHERE ID = @ID)
	BEGIN
		SET @message = @message + '@ID: ' + @ID + ' is not a valid ID for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT [ID], [Level], [EXP], [Age], [HP], [MP], [HP Regen], [MP Regen], [Damage], [Lift / Move], [Damage Reduction], [Dodge], [Crit], [Hit Chance], [Magic Resist], [Chance to Convince], [Lie is believed], [Chance for loot], [Better loot quality], [Avoid overpowered enemies], [Event goes well]
		FROM [Player].[StatsCalc]
		WHERE ID = @ID
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
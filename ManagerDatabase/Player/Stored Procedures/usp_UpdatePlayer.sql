-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 9th September 2016
-- Description:	Updates a player's data
-- =============================================
CREATE PROCEDURE [Player].[usp_UpdatePlayer]
	-- Add the parameters for the stored procedure here
	@ID int,
	@Name varchar(25),
	@Surname varchar(25),
	@Story varchar(100)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_UpdatePlayer: ';

	/* Validation */
	IF @ID IS NULL OR @ID = ''
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Name IS NULL OR @Name = ''
	BEGIN
		SET @message = @message + '@Name cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Surname IS NULL OR @Surname = ''
	BEGIN
		SET @message = @message + '@Surname cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Story IS NULL OR @Story = ''
	BEGIN
		SET @message = @message + '@Story cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[Player] WHERE ID = @ID)
	BEGIN
		SET @message = @message + '@ID: ' + @ID + ' is not a valid player for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Update Data */
		UPDATE       Player.Player
		SET          Name = @Name, Surname = @Surname
		WHERE        ID = @ID

		UPDATE       Player.Story
		SET          Name = @Story
		WHERE        ID = (SELECT Story FROM Player.Player WHERE ID = @ID)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
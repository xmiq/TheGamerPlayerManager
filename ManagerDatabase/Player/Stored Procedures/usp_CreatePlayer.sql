-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 9th September 2016
-- Description:	Creates a new player
-- =============================================
CREATE PROCEDURE [Player].[usp_CreatePlayer]
	-- Add the parameters for the stored procedure here
	@Name varchar(25),
	@Surname varchar(25),
	@Story varchar(100),
	@User varchar(20)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_CreatePlayer: ';

	/* Validation */
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

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @User)
	BEGIN
		SET @message = @message + '@User: ' + @User + ' is not a valid username for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Add Data */
		INSERT INTO [aotxgmr].[Player].[Player]
		([Name], [Surname], [Story], [User])
		VALUES
		(@Name, @Surname, @Story, @User);
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
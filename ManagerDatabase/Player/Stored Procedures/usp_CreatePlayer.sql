-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 9th September 2016
-- Description:	Creates a new player
-- =============================================
CREATE PROCEDURE [Player].[usp_CreatePlayer]
	-- Add the parameters for the stored procedure here
	@Name varchar(25),
	@Surname varchar(25),
	@Story int
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

	IF @Story IS NULL OR @Story = 0
	BEGIN
		SET @message = @message + '@Story cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Player].[Story] WHERE ID = @Story)
	BEGIN
		SET @message = @message + '@Story: ' + @Story + ' is not a valid story for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Add Data */
		INSERT INTO [Player].[Player]
		([Name], [Surname], [Story])
		VALUES
		(@Name, @Surname, @Story);
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
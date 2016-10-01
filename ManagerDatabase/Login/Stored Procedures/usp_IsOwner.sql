-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 1st October 2016
-- Description:	Checks whether the user is the owner
-- =============================================
CREATE PROCEDURE [Login].[usp_IsOwner]
	@Token uniqueidentifier,
	@Username varchar(20)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    /* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Login.usp_Login: ';

	/* Validation */
	IF @Token IS NULL OR @Token = '00000000-0000-0000-0000-000000000000'
	BEGIN
		SET @message = @message + '@Token cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Username IS NULL OR @Username = ''
	BEGIN
		SET @message = @message + '@Username cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Token = @Token)
	BEGIN
		SET @message = @message + 'User is not logged in.';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @Username)
	BEGIN
		SET @message = @message + 'User: ' + @Username + ' does not exist.';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		IF EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Token = @Token AND Username = @Username)
		BEGIN
			SELECT 1;
		END
		ELSE
		BEGIN
			SELECT 0;
		END
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
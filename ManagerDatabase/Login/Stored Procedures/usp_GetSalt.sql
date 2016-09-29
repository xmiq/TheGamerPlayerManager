-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 29th September 2016
-- Description:	Retreives the Salt for Hashing
-- =============================================
CREATE PROCEDURE [Login].[usp_GetSalt]
	@username varchar(20)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Login.usp_GetSalt: ';

	/* Validation */
	IF @username IS NULL OR @username = ''
	BEGIN
		SET @message = @message + '@username cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @username)
	BEGIN
		SET @message = @message + '@username: ' + @username + ' is not a valid user for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT [Salt]
		FROM [Login].[User]
		WHERE Username = @username;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 1st October 2016
-- Description:	Get User Details
-- =============================================
CREATE PROCEDURE [Login].[usp_GetUserDetails]
	@Token uniqueidentifier
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

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Token = @Token)
	BEGIN
		SET @message = @message + 'User is not logged in.';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT [Username], [Name], [Surname], [Email]
		FROM [Login].[User]
		WHERE Token = @Token
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 1st October 2016
-- Description:	Updates Password
-- =============================================
CREATE PROCEDURE [Login].[usp_UpdatePassword]
	@Token uniqueidentifier,
	@OldPassword varchar(300),
	@NewPassword varchar(300)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    /* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Login.usp_UpdatePassword: ';

	/* Validation */
	IF @Token IS NULL OR @Token = '00000000-0000-0000-0000-000000000000'
	BEGIN
		SET @message = @message + '@Token cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @OldPassword IS NULL OR @OldPassword = ''
	BEGIN
		SET @message = @message + '@OldPassword cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @NewPassword IS NULL OR @NewPassword = ''
	BEGIN
		SET @message = @message + '@NewPassword cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Token = @Token)
	BEGIN
		SET @message = @message + 'User is not logged in.';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Token = @Token AND [Password] = @OldPassword)
	BEGIN
		SET @message = @message + 'Password is invalid.';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Update Data */
		UPDATE [Login].[User]
		SET [Password] = @NewPassword
		WHERE (Token = @Token)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
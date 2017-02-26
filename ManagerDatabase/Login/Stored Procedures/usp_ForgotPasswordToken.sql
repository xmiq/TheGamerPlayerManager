-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 15 Feburary 2017
-- Description:	Generates a timed forgot password token
-- =============================================
CREATE PROCEDURE [Login].[usp_ForgotPasswordToken]
	@user VARCHAR(20)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    /* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Login.usp_FrogotPasswordToken: ',
			@email varchar(50);
	DECLARE @return TABLE (ID int, Token uniqueidentifier);

	/* Validation */
	IF @user IS NULL OR @user = ''
	BEGIN
		SET @message = @message + '@user cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @user)
	BEGIN
		SET @message = @message + '@user: ' + @user + ' is not a valid user for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		INSERT INTO [Login].[ForgotPasswordToken] ([User])
		OUTPUT inserted.ID, inserted.Token INTO @return
		VALUES
		(@user)

		SELECT @email = Email
		FROM [Login].[User]
		WHERE Username = @user;

		SELECT ID, Token, @email AS Email
		FROM @return
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 29th September 2016
-- Description:	Logs you into the system
-- =============================================
CREATE PROCEDURE [Login].[usp_Login]
	@Username varchar(20),
	@Password varchar(300)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    /* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Login.usp_Login: ';

	/* Validation */
	IF @Username IS NULL OR @Username = ''
	BEGIN
		SET @message = @message + '@Username cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Password IS NULL OR @Password = ''
	BEGIN
		SET @message = @message + '@Password cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @Username)
	BEGIN
		SET @message = @message + '@Username: ' + @Username + ' is not a valid user for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Check if account is locked */
		/* Logic: Less than 3 tries, or 3 tries or more and 10 minutes have passed  */
		IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @Username AND (Tries < 3 OR (Tries >= 3 AND DATEADD(MINUTE, 10, LastTry) < SYSDATETIME())))
		BEGIN	

			/* Try Login */
			IF EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @Username AND @Password = @Password)
			BEGIN
				/* Login Succeded! Reset Tries */
				UPDATE [Login].[User]
				SET Tries = 0, LastTry = SYSDATETIME()
				WHERE Username = @Username;

				SELECT 1;
			END
			ELSE
			BEGIN
				/* Login Fails! Incrememnt counter */
				UPDATE [Login].[User]
				SET Tries = Tries + 1, LastTry = SYSDATETIME()
				WHERE Username = @Username;
	
				SELECT 0;
			END
		END
		ELSE
		BEGIN
			SELECT -1;
		END
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
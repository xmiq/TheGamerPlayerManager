-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 30th September 2016
-- Description:	Registers a new user.
-- =============================================
CREATE PROCEDURE [Login].[usp_Register]
	@Username varchar(20),
	@Name varchar(20),
	@Surname varchar(20),
	@Email varchar(50),
	@Password varchar(300),
	@Salt char(10)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    /* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Login.usp_Register: ';

	/* Validation */

	IF @Username IS NULL OR @Username = ''
	BEGIN
		SET @message = @message + '@Username cannot be empty';
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

	IF @Email IS NULL OR @Email = ''
	BEGIN
		SET @message = @message + '@Email cannot be empty';
		THROW 50000, @message, 1;
	END

	IF EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @Username)
	BEGIN
		SELECT 0 AS LoginResult;

		SET @message = @message + '@Username: ' + @Username + ' already exists in this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		INSERT INTO [Login].[User] ([Username], [Name], [Surname], [Email], [Password], [Salt], [Token])
		VALUES (@Username, @Name, @Surname, @Email, @Password, @Salt, NEWID())

		SELECT 1 AS LoginResult, Token
		FROM [Login].[User]
		WHERE Username = @Username;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 1st October 2016
-- Description:	Updates User Details
-- =============================================
CREATE PROCEDURE [Login].[usp_UpdateUserDetails]
	@Token uniqueidentifier,
	@Name varchar(20),
	@Surname varchar(20),
	@Email varchar(50)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    /* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Login.usp_UpdateUserDetails: ';

	/* Validation */
	IF @Token IS NULL OR @Token = '00000000-0000-0000-0000-000000000000'
	BEGIN
		SET @message = @message + '@Token cannot be empty';
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

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Token = @Token)
	BEGIN
		SET @message = @message + 'User is not logged in.';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Update Data */
		UPDATE [Login].[User]
		SET [Name] = @Name, [Surname] = @Surname, [Email] = @Email
		WHERE (Token = @Token)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
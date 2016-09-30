﻿-- =============================================
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
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Login.usp_Login: ';

	/* Validation */

	IF @Username IS NULL OR @Username = ''
	IF @Name IS NULL OR @Name = ''
	IF @Surname IS NULL OR @Surname = ''
	IF @Email IS NULL OR @Email = ''

	IF EXISTS (SELECT 1 AS RelatedRecords FROM [Login].[User] WHERE Username = @Username)
	BEGIN
		SELECT 0;

		SET @message = @message + '@Username: ' + @Username + ' already exists in this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		INSERT INTO [Login].[User] ([Username], [Name], [Surname], [Email], [Password], [Salt])
		VALUES (@Username, @Name, @Surname, @Email, @Password, @Salt)

		SELECT 1;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
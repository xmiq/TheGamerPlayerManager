﻿-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 7th October 2016
-- Description:	Returns a story
-- =============================================
CREATE PROCEDURE [Player].[usp_GetStory]
	-- Add the parameters for the stored procedure here
	@ID int
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetStory: ';

	/* Validation */
	IF @ID IS NULL OR @ID = 0
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.Story WHERE ID = @ID)
	BEGIN
		SET @message = @message + '@ID: ' + @ID + ' is not a valid ID for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		SELECT [ID], [Name], [User]
		FROM Player.Story
		WHERE ID = @ID;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
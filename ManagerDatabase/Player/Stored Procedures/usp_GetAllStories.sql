-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 3rd October 2016
-- Description:	Gets the list of all stories
-- =============================================
CREATE PROCEDURE [Player].[usp_GetAllStories]
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    BEGIN TRY
		SELECT [ID], [Name], [User]
		FROM Player.Story
	END TRY
	BEGIN CATCH
		--Implement Error Catching
	END CATCH
END
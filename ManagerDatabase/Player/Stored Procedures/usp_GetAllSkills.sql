-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 26th September 2016
-- Description:	Gets the list of all skills
-- =============================================
CREATE PROCEDURE [Player].[usp_GetAllSkills] (@Story int)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    BEGIN TRY
		SELECT [ID], [Name], [Description]
		FROM Player.Skills
		WHERE [StoryID] = @Story;
	END TRY
	BEGIN CATCH
		--Implement Error Catching
	END CATCH
END
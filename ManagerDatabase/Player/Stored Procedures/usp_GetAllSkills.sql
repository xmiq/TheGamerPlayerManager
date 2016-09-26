-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 26th September 2016
-- Description:	Gets the list of all skills
-- =============================================
CREATE PROCEDURE [Player].[usp_GetAllSkills]
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

    BEGIN TRY
		SELECT [ID], [Name], [Description]
		FROM Player.Skills
	END TRY
	BEGIN CATCH
		--Implement Error Catching
	END CATCH
END
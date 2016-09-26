-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 13th September 2016
-- Description:	Returns a player's skills for the chapter
-- =============================================
CREATE PROCEDURE [Player].[usp_GetSkills]
	-- Add the parameters for the stored procedure here
	@Chapter int,
	@OtherCharLevel int = 1
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_GetSkills: ';
	DECLARE @activeFormula TABLE (ID int, [Active Formula] int);
	DECLARE @activeCostFormula TABLE (ID int, [Active Cost Formula] int);
	DECLARE @passiveFormula TABLE (ID int, [Passive Formula] int);

	/* Validation */
	IF @Chapter IS NULL OR @Chapter = 0
	BEGIN
		SET @message = @message + '@Chapter cannot be empty';
		THROW 50000, @message, 1;
	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.Chapter WHERE ID = @Chapter)
	BEGIN
		SET @message = @message + '@Chapter: ' + @Chapter + ' is not a valid Chapter for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		
		DECLARE @SQL nvarchar(max) = '';

		SELECT @SQL = CONCAT(@SQL, N'WITH Skill AS (SELECT ID, Level AS [Skill Level], Chapter FROM [Player].[SkillStatus] WHERE SkillID = ' 
					+ CAST (ID AS varchar) + '), Stats AS (SELECT ID, Level AS [Character Level], Level - '
					+ CAST(@OtherCharLevel AS varchar) + ' AS [Level Difference] FROM [Player].[Status] WHERE Chapter = '
					+ CAST (@Chapter AS varchar) + ') SELECT '
					+ CAST(ID AS varchar) + ' AS ID, CEILING(' + [Active Formula] + ') AS [Active Formula] FROM [Player].[Status] s JOIN Stats st ON (s.ID = st.ID), Skill sk;')
		FROM [Player].[Skills];

		INSERT INTO @activeFormula
		EXEC (@SQL);

		SET @SQL = '';

		SELECT @SQL = CONCAT(@SQL, N'SELECT '
					+ CAST(ID AS varchar) + ' AS ID, CEILING(' + [Active Cost Formula] + ') AS [Active Cost Formula] FROM [Player].[SkillStatus] s WHERE SkillID = ' + CAST(ID AS varchar) + ';')
		FROM [Player].[Skills];

		INSERT INTO @activeCostFormula
		EXEC (@SQL);

		SET @SQL = '';

		SELECT @SQL = CONCAT(@SQL, N'WITH Skill AS (SELECT ID, Level AS [Skill Level], Chapter FROM [Player].[SkillStatus] WHERE SkillID = ' 
					+ CAST (ID AS varchar) + '), Stats AS (SELECT ID, Level AS [Character Level] FROM [Player].[Status] WHERE Chapter = '
					+ CAST (@Chapter AS varchar) + ') SELECT '
					+ CAST(ID AS varchar) + ' AS ID, CEILING(' + [Passive Formula] + ') AS [Passive Formula] FROM [Player].[Status] s JOIN Stats st ON (s.ID = st.ID) JOIN Skill sk ON (sk.ID = '
					+ CAST(ID AS varchar) + ');')
		FROM [Player].[Skills];

		INSERT INTO @passiveFormula
		EXEC (@SQL);

		SELECT ss.[ID], s.[Name], s.[Description], s.[Type], REPLACE(REPLACE(s.[Active Description Formula], '[Formula]', af.[Active Formula]), '[Cost]', acf.[Active Cost Formula]) AS [Active Description], REPLACE(s.[Passive Description Formula], '[Formula]', pf.[Passive Formula]) AS [Passive Description], ss.[Level], ss.[EXP]
		FROM [Player].[SkillStats] ss
		JOIN [Player].[Skills] s ON (ss.SkillID = s.ID)
		LEFT OUTER JOIN @activeFormula af ON (af.ID = s.ID)
		LEFT OUTER JOIN @activeCostFormula acf ON (acf.ID = s.ID)
		LEFT OUTER JOIN @passiveFormula pf ON (pf.ID = s.ID)
		WHERE ss.Chapter = @Chapter;
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
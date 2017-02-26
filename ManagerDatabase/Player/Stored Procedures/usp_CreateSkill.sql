-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 28th September 2016
-- Description:	Creates a skill
-- =============================================
CREATE PROCEDURE [Player].[usp_CreateSkill]
	-- Add the parameters for the stored procedure here
	@Story int,
	@Name varchar(50),
	@Description varchar(400),
	@Type int,
	@ActiveDescriptionFormula varchar(200) = '',
	@PassiveDescriptionFormula varchar(200) = '',
	@ActiveFormula varchar(200) = '',
	@ActiveCostFormula varchar(200) = '',
	@PassiveFormula varchar(200) = ''
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_CreateSkill: ';

	/* Validation */
	IF @Name IS NULL OR @Name = ''
	BEGIN
		SET @message = @message + '@Name cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Description IS NULL OR @Description = ''
	BEGIN
		SET @message = @message + '@Description cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Type IS NULL OR @Type = 0
	BEGIN
		SET @message = @message + '@Type cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Type = 1 AND (@ActiveDescriptionFormula IS NULL OR @ActiveDescriptionFormula = '')
	BEGIN
		SET @message = @message + '@ActiveDescriptionFormula cannot be empty';
		THROW 50000, @message, 1;
	END
	ELSE IF @Type = 2 AND (@PassiveDescriptionFormula IS NULL OR @PassiveDescriptionFormula = '') AND (@ActiveDescriptionFormula IS NOT NULL AND @ActiveDescriptionFormula <> '')
	BEGIN
		SET @message = @message + '@PassiveDescriptionFormula cannot be empty while @ActiveDescriptionFormula is filled';
		THROW 50000, @message, 1;
	END
	ELSE IF @Type = 3 AND ((@ActiveDescriptionFormula IS NULL OR @ActiveDescriptionFormula = '') AND (@PassiveDescriptionFormula IS NULL OR @PassiveDescriptionFormula = ''))
	BEGIN
		SET @message = @message + '@ActiveDescriptionFormula and @PassiveDescriptionFormula cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Type = 1 AND ((@ActiveFormula IS NULL OR @ActiveFormula = '') AND (@ActiveCostFormula IS NULL OR @ActiveCostFormula = ''))
	BEGIN
		SET @message = @message + '@ActiveFormula and @ActiveCostFormula must be filled';
		THROW 50000, @message, 1;
	END
	ELSE IF @Type = 2 AND (@PassiveFormula IS NULL OR @PassiveFormula = '') AND ((@ActiveFormula IS NOT NULL OR @ActiveFormula <> '') AND (@ActiveCostFormula IS NOT NULL AND @ActiveCostFormula <> ''))
	BEGIN
		SET @message = @message + '@PassiveFormula cannot be empty while @ActiveFormula or @ActiveCostFormula is filled';
		THROW 50000, @message, 1;
	END
	ELSE IF @Type = 3 AND ((@ActiveFormula IS NULL OR @ActiveFormula = '') AND (@ActiveCostFormula IS NULL OR @ActiveCostFormula = '')) AND (@PassiveFormula IS NULL OR @PassiveFormula = '')
	BEGIN
		SET @message = @message +  '@ActiveFormula, @ActiveCostFormula and @PassiveFormula must be filled';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		INSERT INTO Player.Skills ([StoryID], [Name], [Description], [Type], [Active Description Formula], [Passive Description Formula], [Active Formula], [Active Cost Formula], [Passive Formula])
		VALUES (@Story ,@Name, @Description, @Type, @ActiveDescriptionFormula, @PassiveDescriptionFormula, @ActiveFormula, @ActiveCostFormula, @PassiveFormula)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	  THROW;
	END CATCH
END
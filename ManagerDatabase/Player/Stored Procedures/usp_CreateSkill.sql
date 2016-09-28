-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 28th September 2016
-- Description:	Creates a skill
-- =============================================
CREATE PROCEDURE [Player].[usp_CreateSkill]
	-- Add the parameters for the stored procedure here
	@Name varchar(50),
	@Description varchar(400),
	@Type int,
	@ActiveDescriptionFormula varchar(200),
	@PassiveDescriptionFormula varchar(200),
	@ActiveFormula varchar(200),
	@ActiveCostFormula varchar(200),
	@PassiveFormula varchar(200)
AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements. */
	SET NOCOUNT ON;

	/* Declare variables */
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_CreateSkill: ';

	/* Validation */
	IF @Name IS NULL OR @Name = ''	BEGIN		SET @message = @message + '@Name cannot be empty';		THROW 50000, @message, 1;	END
	IF @Description IS NULL OR @Description = ''	BEGIN		SET @message = @message + '@Description cannot be empty';		THROW 50000, @message, 1;	END
	IF @Type IS NULL OR @Type = 0	BEGIN		SET @message = @message + '@Type cannot be empty';		THROW 50000, @message, 1;	END
	IF (@ActiveDescriptionFormula IS NULL OR @ActiveDescriptionFormula = '') AND (@PassiveDescriptionFormula IS NULL OR @PassiveDescriptionFormula = '')	BEGIN		SET @message = @message + '@ActiveDescriptionFormula @PassiveDescriptionFormula cannot be both empty';		THROW 50000, @message, 1;	END
	IF ((@ActiveFormula IS NULL OR @ActiveFormula = '') AND (@ActiveCostFormula IS NULL OR @ActiveCostFormula = '')) AND (@PassiveFormula IS NULL OR @PassiveFormula = '')	BEGIN		SET @message = @message + 'Either @ActiveFormula and @ActiveCostFormula or @PassiveFormula must be filled';		THROW 50000, @message, 1;	END

	BEGIN TRY
		/* Get Data */
		INSERT INTO Player.Skills ([Name], [Description], [Type], [Active Description Formula], [Passive Description Formula], [Active Formula], [Active Cost Formula], [Passive Formula])
		VALUES (@Name, @Description, @Type, @ActiveDescriptionFormula, @PassiveDescriptionFormula, @ActiveFormula, @ActiveCostFormula, @PassiveFormula)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
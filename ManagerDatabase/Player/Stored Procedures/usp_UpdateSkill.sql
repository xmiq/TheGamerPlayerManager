-- =============================================
-- Author:		Jonathan Camilleri
-- Create date: 27th September 2016
-- Description:	Updates a skill
-- =============================================
CREATE PROCEDURE [Player].[usp_UpdateSkill]
	-- Add the parameters for the stored procedure here
	@ID int,
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
	DECLARE @message varchar(4000) = 'An error has been thrown for stored procedure Player.usp_UpdateSkill: ';

	/* Validation */
	IF @ID IS NULL OR @ID = 0
	BEGIN
		SET @message = @message + '@ID cannot be empty';
		THROW 50000, @message, 1;
	END

	IF @Name IS NULL OR @Name = ''	BEGIN		SET @message = @message + '@Name cannot be empty';		THROW 50000, @message, 1;	END
	IF @Description IS NULL OR @Description = ''	BEGIN		SET @message = @message + '@Description cannot be empty';		THROW 50000, @message, 1;	END
	IF @Type IS NULL OR @Type = 0	BEGIN		SET @message = @message + '@Type cannot be empty';		THROW 50000, @message, 1;	END
	IF (@ActiveDescriptionFormula IS NULL OR @ActiveDescriptionFormula = '') AND (@PassiveDescriptionFormula IS NULL OR @PassiveDescriptionFormula = '')	BEGIN		SET @message = @message + '@ActiveDescriptionFormula @PassiveDescriptionFormula cannot be both empty';		THROW 50000, @message, 1;	END
	IF ((@ActiveFormula IS NULL OR @ActiveFormula = '') AND (@ActiveCostFormula IS NULL OR @ActiveCostFormula = '')) AND (@PassiveFormula IS NULL OR @PassiveFormula = '')	BEGIN		SET @message = @message + 'Either @ActiveFormula and @ActiveCostFormula or @PassiveFormula must be filled';		THROW 50000, @message, 1;	END

	IF NOT EXISTS (SELECT 1 AS RelatedRecords FROM Player.Skills WHERE ID = @ID)
	BEGIN
		SET @message = @message + '@ID: ' + @ID + ' is not a valid ID for this database';
		THROW 50000, @message, 1;
	END

	BEGIN TRY
		/* Get Data */
		UPDATE Player.Skills
		SET [Name] = @Name, [Description] = @Description, [Type] = @Type, [Active Description Formula] = @ActiveDescriptionFormula, [Passive Description Formula] = @PassiveDescriptionFormula, [Active Formula] = @ActiveFormula, [Active Cost Formula] = @ActiveCostFormula, [Passive Formula] = @PassiveFormula
		WHERE (ID = @ID)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
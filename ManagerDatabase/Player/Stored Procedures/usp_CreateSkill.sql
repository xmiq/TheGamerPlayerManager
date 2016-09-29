﻿-- =============================================
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
	IF @Name IS NULL OR @Name = ''
	IF @Description IS NULL OR @Description = ''
	IF @Type IS NULL OR @Type = 0
	IF (@ActiveDescriptionFormula IS NULL OR @ActiveDescriptionFormula = '') AND (@PassiveDescriptionFormula IS NULL OR @PassiveDescriptionFormula = '')
	IF ((@ActiveFormula IS NULL OR @ActiveFormula = '') AND (@ActiveCostFormula IS NULL OR @ActiveCostFormula = '')) AND (@PassiveFormula IS NULL OR @PassiveFormula = '')

	BEGIN TRY
		/* Get Data */
		INSERT INTO Player.Skills ([Name], [Description], [Type], [Active Description Formula], [Passive Description Formula], [Active Formula], [Active Cost Formula], [Passive Formula])
		VALUES (@Name, @Description, @Type, @ActiveDescriptionFormula, @PassiveDescriptionFormula, @ActiveFormula, @ActiveCostFormula, @PassiveFormula)
	END TRY
	BEGIN CATCH
	  -- Implement Error Catching
	END CATCH
END
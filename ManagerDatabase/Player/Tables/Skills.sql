CREATE TABLE [Player].[Skills] (
    [ID]                          INT           CONSTRAINT [DF_Skills_ID] DEFAULT (NEXT VALUE FOR [Player].[SeqSkills]) NOT NULL,
    [StoryID]                     INT           NOT NULL,
    [Name]                        VARCHAR (50)  NOT NULL,
    [Description]                 VARCHAR (400) NOT NULL,
    [Type]                        INT           NOT NULL,
    [Active Description Formula]  VARCHAR (200) NULL,
    [Passive Description Formula] VARCHAR (200) NULL,
    [Active Formula]              VARCHAR (200) NULL,
    [Active Cost Formula]         VARCHAR (200) NULL,
    [Passive Formula]             VARCHAR (200) NULL,
    CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Skills_SkillType] FOREIGN KEY ([Type]) REFERENCES [PlayerDomain].[SkillType] ([ID]),
    CONSTRAINT [FK_Skills_Story] FOREIGN KEY ([StoryID]) REFERENCES [Player].[Story] ([ID])
);




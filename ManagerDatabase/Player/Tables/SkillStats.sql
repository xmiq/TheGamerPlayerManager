﻿CREATE TABLE [Player].[SkillStats] (
    [ID]      INT CONSTRAINT [DF_SkillStats_ID] DEFAULT (NEXT VALUE FOR [Player].[SeqSkillStats]) NOT NULL,
    [SkillID] INT NOT NULL,
    [Level]   INT NOT NULL,
    [Chapter] INT NOT NULL,
    [Player]  INT NOT NULL,
    [EXP]     INT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SkillStats] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SkillStats_Chapter] FOREIGN KEY ([Chapter]) REFERENCES [Player].[Chapter] ([ID]),
    CONSTRAINT [FK_SkillStats_Player] FOREIGN KEY ([Player]) REFERENCES [Player].[Player] ([ID]),
    CONSTRAINT [FK_SkillStats_Skills] FOREIGN KEY ([SkillID]) REFERENCES [Player].[Skills] ([ID])
);




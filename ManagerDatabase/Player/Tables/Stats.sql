CREATE TABLE [Player].[Stats] (
    [ID]           INT CONSTRAINT [DF_Stats_ID] DEFAULT (NEXT VALUE FOR [Player].[SeqStats]) NOT NULL,
    [Level]        INT NOT NULL,
    [EXP]          INT NOT NULL,
    [Age]          INT NOT NULL,
    [Strength]     INT NOT NULL,
    [Vitality]     INT NOT NULL,
    [Constitution] INT NOT NULL,
    [Dexterity]    INT NOT NULL,
    [Accuracy]     INT NOT NULL,
    [Inteligence]  INT NOT NULL,
    [Wisdom]       INT NOT NULL,
    [Charisma]     INT NOT NULL,
    [Luck]         INT NOT NULL,
    [Player]       INT NOT NULL,
    [Chapter]      INT NOT NULL,
    CONSTRAINT [PK_Stats] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Stats_Chapter] FOREIGN KEY ([Chapter]) REFERENCES [Player].[Chapter] ([ID]),
    CONSTRAINT [FK_Stats_Player] FOREIGN KEY ([Player]) REFERENCES [Player].[Player] ([ID])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Player_Stats_ChapterPlayer]
    ON [Player].[Stats]([Player] ASC, [Chapter] ASC);


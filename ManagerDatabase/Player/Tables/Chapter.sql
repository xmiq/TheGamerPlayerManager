CREATE TABLE [Player].[Chapter] (
    [ID]     INT CONSTRAINT [DF_Chapter_ID] DEFAULT (NEXT VALUE FOR [Player].[SeqChapter]) NOT NULL,
    [Player] INT NOT NULL,
    [Number] INT NOT NULL,
    CONSTRAINT [PK_Chapter] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Chapter_Player] FOREIGN KEY ([Player]) REFERENCES [Player].[Player] ([ID])
);


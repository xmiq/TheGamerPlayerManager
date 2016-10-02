CREATE TABLE [Player].[Player] (
    [ID]      INT          CONSTRAINT [DF_Player_ID] DEFAULT (NEXT VALUE FOR [Player].[SeqPlayer]) NOT NULL,
    [Name]    VARCHAR (25) NOT NULL,
    [Surname] VARCHAR (25) NOT NULL,
    [Story]   INT          NOT NULL,
    CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Player_Story] FOREIGN KEY ([Story]) REFERENCES [Player].[Story] ([ID])
);




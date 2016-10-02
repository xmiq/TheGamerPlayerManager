CREATE TABLE [Player].[Story] (
    [ID]   INT           CONSTRAINT [DF_Story_ID] DEFAULT (NEXT VALUE FOR [Player].[SeqStory]) NOT NULL,
    [Name] VARCHAR (100) NOT NULL,
    [User] VARCHAR (20)  NOT NULL,
    CONSTRAINT [PK_Story] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Story_User] FOREIGN KEY ([User]) REFERENCES [Login].[User] ([Username])
);


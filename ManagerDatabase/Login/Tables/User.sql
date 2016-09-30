CREATE TABLE [Login].[User] (
    [Username]    VARCHAR (20)     NOT NULL,
    [Name]        VARCHAR (20)     NOT NULL,
    [Surname]     VARCHAR (20)     NOT NULL,
    [Email]       VARCHAR (50)     NOT NULL,
    [Password]    VARCHAR (300)    NOT NULL,
    [Salt]        CHAR (10)        NOT NULL,
    [Tries]       INT              CONSTRAINT [DF_User_Tries] DEFAULT ((0)) NOT NULL,
    [LastTry]     DATE             NULL,
    [Token]       UNIQUEIDENTIFIER NULL,
    [TokenUnique] AS               (isnull(CONVERT([varchar](36),[Token]),[Username])),
    CONSTRAINT [PK_User#] PRIMARY KEY CLUSTERED ([Username] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Login_User_Token]
    ON [Login].[User]([TokenUnique] ASC);


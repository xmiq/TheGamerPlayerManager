CREATE TABLE [Login].[User] (
    [Username] VARCHAR (20)  NOT NULL,
    [Name]     VARCHAR (20)  NOT NULL,
    [Surname]  VARCHAR (20)  NOT NULL,
    [Email]    VARCHAR (50)  NOT NULL,
    [Password] VARCHAR (300) NOT NULL,
    [Salt]     CHAR (10)     NOT NULL,
    [Tries]    INT           CONSTRAINT [DF_User_Tries] DEFAULT ((0)) NOT NULL,
    [LastTry]  DATE          NULL,
    CONSTRAINT [PK_User#] PRIMARY KEY CLUSTERED ([Username] ASC)
);




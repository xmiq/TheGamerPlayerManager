CREATE TABLE [Login].[User] (
    [Username] VARCHAR (20) NOT NULL,
    [Name]     VARCHAR (20) NOT NULL,
    [Surname]  VARCHAR (20) NOT NULL,
    [Email]    VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_User#] PRIMARY KEY CLUSTERED ([Username] ASC)
);


CREATE TABLE [Login].[ForgotPasswordToken] (
    [ID]     INT              CONSTRAINT [DF_ForgotPasswordToken_ID] DEFAULT (NEXT VALUE FOR [Login].[SeqForgotPasswordToken]) NOT NULL,
    [User]   VARCHAR (20)     NOT NULL,
    [Token]  UNIQUEIDENTIFIER CONSTRAINT [DF_ForgotPasswordToken_Token] DEFAULT (newid()) NOT NULL,
    [Expiry] DATETIME         CONSTRAINT [DF_ForgotPasswordToken_Expiry] DEFAULT (dateadd(day,(1),getdate())) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ForgotPasswordToken_User] FOREIGN KEY ([User]) REFERENCES [Login].[User] ([Username])
);




GO
CREATE NONCLUSTERED INDEX [IX_ForgotPasswordToken_User]
    ON [Login].[ForgotPasswordToken]([User] ASC);


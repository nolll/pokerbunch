﻿CREATE TABLE [dbo].[Game] (
    [GameId]     INT           IDENTITY (1, 1) NOT NULL,
    [Date]       DATE          NOT NULL,
    [Location]   NVARCHAR (50) NOT NULL,
    [Timestamp]  DATETIME      CONSTRAINT [DF_Game_Timestamp] DEFAULT (getdate()) NOT NULL,
    [Status]     INT           NOT NULL,
    [HomegameId] INT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([GameId] ASC)
);

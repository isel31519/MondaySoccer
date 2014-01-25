CREATE TABLE [dbo].[Game]
(
	[GameID] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATE NOT NULL, 
    [HomeTeam] BIGINT NOT NULL, 
    [AwayTeam] BIGINT NOT NULL, 
    [HomeScore] INT NULL, 
    [AwayScore] INT NULL, 
    [MVP] BIGINT NULL references player(playerID), 
    [StateCod] NVARCHAR(2) NOT NULL references State(Code),
	CONSTRAINT [FK_GameSheet_ToTeam] FOREIGN KEY ([HomeTeam]) REFERENCES [Team]([TeamID]), 
    CONSTRAINT [FK_Game_ToTeam2] FOREIGN KEY ([AwayTeam]) REFERENCES [Team]([TeamID])
)

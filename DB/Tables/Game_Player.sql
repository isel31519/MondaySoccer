CREATE TABLE [Game_Player]
(
	[InvitationPlayerID] BIGINT NOT NULL PRIMARY KEY identity, 
    [GameID] BIGINT NOT NULL references Game([GameID]), 
    [PlayerID] BIGINT NOT NULL references Player(PlayerID), 
    [Comments] NVARCHAR(250) NULL, 
    [Golos] INT NULL, 
    [CreationDate] DATETIME NOT NULL, 
    [IsSubstitute] BIT NOT NULL, 
    CONSTRAINT [UK_Invitation_Player_Column] Unique ([GameID],[PlayerID]) 
)

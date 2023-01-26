-- 本局游戏最大玩家数量
ChessConst.BattlePlayerMax = 8
-- 本局游戏最大种族类型数量
ChessConst.BattlePiecesRaceMax = 5
-- 本局游戏最大棋子阵容列表最大数量
ChessConst.BattlePiecesMax = 7
-- 本局游戏玩家最大选取英雄数量
ChessConst.BattlePickHeroMax = 4

-- 本局游戏对应状态
ChessConst.BattleStatusType = {
    Start = 1,
    PickHero = 2,
    PickPieces = 3,
    Battle = 4,
    End = 5
}

-- 棋子对应种族类型
ChessConst.PiecesRaceType = {
    None = 0,
    Fish = 1,
    Pig = 2,
    Demon = 3,
    Machine = 4,
    Pirate = 5,
    Dragon = 6,
    Beast = 7,
    Element = 8
}

-- todoChess 酒馆状态
ChessConst.BartenderStatusType = {
    Normal = 1,
    Freeze = 2
}

-- 英雄技能类型
ChessConst.HeroSkillType = {
    Active = 1,
    Passive = 2
}

-- 英雄技能状态
ChessConst.HeroSkillStatusType = {
    Unuse = 1,
    Use = 2
}

-- 玩家当前状态
ChessConst.PlayerStatusType = {
    Alive = 1,
    Win = 2,
    Dead = 3,
}

-- 棋子技能类型
ChessConst.PiecesSkillType = {
    BattleCry = 1,
    Dead = 2,
    Buff = 3,
}

-- 棋子技能状态
ChessConst.PiecesSkillStatusType = {
    Unuse = 1,
    Use = 2
}

-- 棋子状态
ChessConst.PiecesStatusType = {
    Normal = 1,
    Freeze = 2,
    Sleep = 3
}


local EVENT_LUA = 100000
EVENT = 
{
	CHESS_STAGE_START = EVENT_LUA + 9001,
	CHESS_STAGE_PICK_HERO = EVENT_LUA + 9002,
	CHESS_STAGE_PICK_PIECES = EVENT_LUA + 9003,
	CHESS_STAGE_BATTLE = EVENT_LUA + 9004,
	CHESS_STAGE_END = EVENT_LUA + 9005,
}
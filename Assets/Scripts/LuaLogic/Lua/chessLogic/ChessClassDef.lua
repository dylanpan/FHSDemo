local globalMT = getmetatable(_G)
local dummyMT = { }
setmetatable(_G, dummyMT)

ChessConst = {}
ChessUtil = {}
GameController = {}

BartenderController = {}
HeroController = {}
PiecesController = {}
PlayerController = {}
BattleController = {}

Bartender = bclass("Bartender")
Hero = bclass("Hero")
HeroSkill = bclass("HeroSkill")
HeroPool = bclass("HeroPool")
Pieces = bclass("Pieces")
PiecesSkill = bclass("PiecesSkill")
PiecesPool = bclass("PiecesPool")
Player = bclass("Player")
Buff = bclass("Buff")
AtkBuff = bclass("AtkBuff")
HpBuff = bclass("HpBuff")

setmetatable(_G, globalMT)

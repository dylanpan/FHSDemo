function Player:initialize(name, bartenderId)
    ChessUtil.Log("Player")

    -- 酒馆
    self.bartender = Bartender:new(bartenderId)
    -- 英雄
    self.hero = nil
    -- 待选英雄列表
    self.heroList = {}
    -- 金币
    self.currency = ChessUtil.GetPlayerConfigData().currency
    -- 阵容列表上限
    self.piecesListLength = ChessUtil.GetPlayerConfigData().piecesListLength
    -- 阵容列表
    self.piecesList = {}
    -- 手牌列表上限
    self.cardsListLength = ChessUtil.GetPlayerConfigData().cardsListLength
    -- 手牌列表
    self.cardsList = {}
    -- 状态
    self.status = ChessConst.PlayerStatusType.Alive
    -- 名称
    self.name = name
end

-- 设置玩家待选英雄列表
function Player:SetHeroList(heroList)
    self.heroList = heroList

    -- testChess 随机选取一个英雄
    HeroController.PickHero(self, self.heroList[math.random(1, ChessConst.BattlePickHeroMax)])
end

-- 设置玩家英雄
function Player:SetHero(hero)
    self.hero = hero
end

-- 玩家阵容列表是否有空余位置
function Player:CheckPiecesListIsFree()
    return #self.piecesList < self.piecesListLength
end

-- 玩家手牌是否有空余位置
function Player:CheckCardsListIsFree()
    return #self.cardsList < self.cardsListLength
end

-- 判断玩家的金币是否足够
function Player:CheckCurrencyIsEnough(cost)
    return self.currency >= cost
end

function Player:IncreasePiecesToCardsList(pieces)
    table.insert(self.cardsList, pieces)
end

function Player:ReducePiecesFromCardsList(pieces)
    for i, v in ipairs(self.cardsList) do
        -- todoChess 需要存在唯一 id
        if v.id == pieces.id then
            table.remove(self.cardsList, pieces)
            break
        end
    end
end

function Player:IncreasePiecesToPiecesList(pieces)
    table.insert(self.piecesList, pieces)
end

function Player:ReducePiecesFromPiecesList(pieces)
    for i, v in ipairs(self.piecesList) do
        -- todoChess 需要存在唯一 id
        if v.id == pieces.id then
            table.remove(self.piecesList, pieces)
            break
        end
    end
end

-- todoChess 补充棋子权重到配置表中
function Player:GetPiecesListWeight()
    -- todoChess 补充棋子权重求和
    return #self.piecesList * 2
end

function Player:GetInfo()
    local info = {}
    info.name = self.name
    info.bartender = {}
    info.bartender.id = self.bartender.id
    info.bartender.level = self.bartender.level
    info.hero = {}
    info.hero.id = self.hero.id
    info.hero.hp = self.hero.hp
    info.piecesList = {}
    for i, pieces in ipairs(self.piecesList) do
        local tmp = {}
        tmp.id = pieces.id
        tmp.hp = pieces.hp
        tmp.atk = pieces.atk
        tmp.level = pieces.level
        table.insert(info.piecesList, tmp)
    end
    return info
end

return Player
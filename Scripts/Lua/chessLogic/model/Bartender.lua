function Bartender:initialize(id)
    ChessUtil.Log("Bartender")

    -- 酒馆ID
    self.id = id
    -- 酒馆名称
    self.name = ChessUtil.GetBartenderSkinConfigData()[self.id].name
    -- 酒馆皮肤
    self.skin = ChessUtil.GetBartenderSkinConfigData()[self.id].skin
    -- 酒馆等级
    self.level = 1
    -- 酒馆升级消耗
    self.cost = ChessUtil.GetBartenderLevelConfigData()[self.level].cost
    -- 酒馆阵容列表上限
    self.piecesListLength = ChessUtil.GetBartenderLevelConfigData()[self.level].piecesListLength
    -- 酒馆阵容列表
    self.piecesList = {}
end

-- 生成酒馆阵容列表
function Bartender:GeneratePiecesList()
    self.piecesList = BartenderController.GeneratePiecesList(self.level, self.piecesListLength)
end

-- 将酒馆的棋子放置回棋子池子
function Bartender:RecyclePiecesList()
    BartenderController.RecyclePiecesList(self.piecesList)
    self.piecesList = {}
end

-- 设置酒馆状态
function Bartender:SetBartenderStatus(status)
    self.status = status
    -- 设置棋子状态，棋子冻结状态下不放置回池子（池子中棋子的数量没有恢复）
    for i, pieces in ipairs(self.piecesList) do
        pieces.status = status
    end
end

return Bartender
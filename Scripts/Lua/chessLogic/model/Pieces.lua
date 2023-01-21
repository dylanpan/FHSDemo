function Pieces:initialize(id)
    ChessUtil.Log("Pieces")

    -- 棋子ID
    self.id = id
    -- 棋子名称
    self.name = ChessUtil.GetPiecesConfigData()[self.id].name
    -- 棋子皮肤
    self.skin = ChessUtil.GetPiecesConfigData()[self.id].skin
    -- 棋子种族
    self.race = ChessUtil.GetPiecesConfigData()[self.id].race
    -- 棋子对应酒馆星级
    self.level = ChessUtil.GetPiecesConfigData()[self.id].level
    -- 棋子当前数量
    self.num = ChessUtil.GetPiecesConfigData()[self.id].num
    -- 棋子最大数量
    self.maxNum = ChessUtil.GetPiecesConfigData()[self.id].maxNum
    -- 棋子消耗
    self.cost = ChessUtil.GetPiecesConfigData()[self.id].cost
    -- 棋子回收
    self.receive = ChessUtil.GetPiecesConfigData()[self.id].receive
    -- 棋子生命值
    self.hp = ChessUtil.GetPiecesConfigData()[self.id].hp
    -- 棋子攻击力
    self.atk = ChessUtil.GetPiecesConfigData()[self.id].atk
    -- 棋子技能ID
    self.skillId = ChessUtil.GetPiecesConfigData()[self.id].skillId
    -- 棋子技能
    self.skill = PiecesSkill:new(self.skillId)
    -- 棋子状态
    self.status = ChessConst.PiecesStatusType.Normal
end

function Pieces:IncreaseNum()
    if self.num < self.maxNum then
        self.num = self.num + 1
    end
end

function Pieces:ReduceNum()
    if self.num > 0 then
        self.num = self.num - 1
    end
end

function Pieces:IsAlive()
    return self.hp > 0
end

-- 判断棋子的状态是否可以购买
function Pieces:CheckStatusCanBuy()
    return self.status ~= ChessConst.PiecesStatusType.Sleep
end

return Pieces
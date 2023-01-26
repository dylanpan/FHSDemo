function AtkBuff:initialize(id)
    ChessUtil.Log("AtkBuff")

    -- Buff ID
    self.id = id
    -- Buff 增加值
    self.add = ChessUtil.GetAtkBuffConfigData()[self.id].add
    -- Buff 增加种族
    self.raceList = ChessUtil.GetAtkBuffConfigData()[self.id].raceList
    -- Buff 增加数量
    self.num = ChessUtil.GetAtkBuffConfigData()[self.id].num
end

return AtkBuff
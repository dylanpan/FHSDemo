function HpBuff:initialize(id)
    ChessUtil.Log("HpBuff")

    -- Buff ID
    self.id = id
    -- Buff 增加值
    self.add = ChessUtil.GetHpBuffConfigData()[self.id].add
    -- Buff 增加种族
    self.raceList = ChessUtil.GetHpBuffConfigData()[self.id].raceList
    -- Buff 增加数量
    self.num = ChessUtil.GetHpBuffConfigData()[self.id].num
end

return HpBuff
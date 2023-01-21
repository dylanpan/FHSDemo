function Buff:initialize(list)
    ChessUtil.Log("Buff")

    -- Buff 增加攻击力 ID
    self.atkBuffId = list[1]
    -- Buff 增加攻击力
    self.atkBuff = AtkBuff:new(self.atkBuffId)

    -- Buff 增加生命值 ID
    self.addHpId = list[2]
    -- Buff 增加生命值
    self.hpBuff = HpBuff:new(self.addHpId)
end

return Buff
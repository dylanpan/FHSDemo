function Hero:initialize(id)
    ChessUtil.Log("Hero")

    -- 英雄ID
    self.id = id
    -- 英雄名称
    self.name = ChessUtil.GetHeroConfigData()[self.id].name
    -- 英雄皮肤
    self.skin = ChessUtil.GetHeroConfigData()[self.id].skin
    -- 英雄生命值
    self.hp = ChessUtil.GetHeroConfigData()[self.id].hp
    -- 英雄攻击力
    self.atk = ChessUtil.GetHeroConfigData()[self.id].atk
    -- 英雄数量
    self.num = ChessUtil.GetHeroConfigData()[self.id].num
    -- 英雄技能
    self.skillId = ChessUtil.GetHeroConfigData()[self.id].skillId
    -- 英雄技能
    self.skill = HeroSkill:new(self.skillId)
end

function Hero:ReduceNum()
    if self.num > 0 then
        self.num = self.num - 1
    end
end

return Hero
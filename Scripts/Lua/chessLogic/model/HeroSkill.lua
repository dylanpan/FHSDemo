function HeroSkill:initialize(id)
    ChessUtil.Log("HeroSkill")

    -- 英雄技能ID
    self.id = id
    -- 英雄技能名称
    self.name = ChessUtil.GetHeroSkillConfigData()[self.id].name
    -- 英雄技能皮肤
    self.skin = ChessUtil.GetHeroSkillConfigData()[self.id].skin
    -- 英雄技能消耗
    self.cost = ChessUtil.GetHeroSkillConfigData()[self.id].cost
    -- 英雄技能类型
    self.type = ChessUtil.GetHeroSkillConfigData()[self.id].type
    -- 英雄技能效果 Buff ID
    self.buffIdList = ChessUtil.GetHeroSkillConfigData()[self.id].buffIdList
    -- 英雄技能效果 Buff
    self.buff = Buff:new(self.buffIdList)
    -- 英雄技能状态
    self.status = ChessConst.HeroSkillStatusType.Unuse

end

return HeroSkill
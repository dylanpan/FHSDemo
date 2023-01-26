function PiecesSkill:initialize(id)
    ChessUtil.Log("PiecesSkill")

    -- 棋子技能ID
    self.id = id
    -- 棋子技能名称
    self.name = ChessUtil.GetPiecesSkillConfigData()[self.id].name
    -- 棋子技能皮肤
    self.skin = ChessUtil.GetPiecesSkillConfigData()[self.id].skin
    -- 棋子技能类型
    self.type = ChessUtil.GetPiecesSkillConfigData()[self.id].type
    -- 棋子效果 Buff ID
    self.buffIdList = ChessUtil.GetPiecesSkillConfigData()[self.id].buffIdList
    -- 棋子效果 Buff
    self.buff = Buff:new(self.buffIdList)
    -- 棋子技能状态
    self.status = ChessConst.PiecesSkillStatusType.Unuse
end

return PiecesSkill
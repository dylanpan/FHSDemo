function HeroPool:initialize()
    ChessUtil.Log("HeroPool")

    -- 英雄池子
    self.idPool = {}
    self.dataPool = {}
    
	-- 生成本局游戏英雄池子
    for heroId, heroData in pairs(ChessUtil.GetHeroConfigData()) do
        table.insert(self.idPool, heroId)
        self.dataPool[heroId] = Hero:new(heroId)
    end
end

function HeroPool:ReduceHeroNum(heroId)
    if self.dataPool[heroId] ~= nil then
        self.dataPool[heroId]:ReduceNum()
    end
end

return HeroPool
local isInit = false
local heroPool = nil

function HeroController.Init()
	HeroController.isInit = true
	ChessUtil.Log("HeroController")
	HeroController.RegisterEvents()

	heroPool = HeroPool:new()
	
	-- 为每一个玩家提供抽取指定数量的待选英雄列表
	local playerList = PlayerController.GetPlayerList()
	for i, player in pairs(playerList) do
		player:SetHeroList(HeroController.GetHeroListFromPool())
	end
    ChessUtil.SendEvent(EVENT.CHESS_STAGE_PICK_PIECES)
end

function HeroController.RegisterEvents()
end

function HeroController.UnregisterEvents()
end

function HeroController.IsInit()
	return HeroController.isInit
end

-- 从英雄池中抽取指定数量的待选英雄列表
function HeroController.GetHeroListFromPool()
	local heroList = {}
	-- 从英雄池子中获取对应英雄ID 随机列表
	local tmpHeroIdList = ChessUtil.GetRandomList(heroPool.idPool)
	for i, heroId in pairs(tmpHeroIdList) do
		local hero = heroPool.dataPool[heroId]
		if hero ~= nil and hero.num > 0 then
			table.insert(heroList, hero)
			-- 减少池子中的英雄
			heroPool:ReduceHeroNum(heroId)
			if #heroList >= ChessConst.BattlePickHeroMax then
				break
			end
		end
	end
	return heroList
end

-- 设置玩家选取的英雄
function HeroController.PickHero(player, hero)
	player:SetHero(hero)
end

function HeroController.Destroy()
	HeroController.UnregisterEvents()
end
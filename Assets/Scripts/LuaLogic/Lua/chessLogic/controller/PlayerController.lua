local isInit = false
local playerList = {}

function PlayerController.Init()
	PlayerController.isInit = true
    ChessUtil.Log("PlayerController")
	PlayerController.RegisterEvents()

	-- testChess 新增自己
	local bartenderId = 1
	local player = Player:new("Player_A", bartenderId)
	table.insert(playerList, player)
	-- testChess 新增7个 AI
	local aiBartenderId = 2
	for i = 1, ChessConst.BattlePlayerMax - 1 do
		local aiPlayer = Player:new("AI_" .. i, aiBartenderId)
		table.insert(playerList, aiPlayer)
	end
    ChessUtil.SendEvent(EVENT.CHESS_STAGE_PICK_HERO)
end

function PlayerController.RegisterEvents()
end

function PlayerController.UnregisterEvents()
end

function PlayerController.IsInit()
	return PlayerController.isInit
end

function PlayerController.GetPlayerList()
	table.sort(playerList, function(playerA, playerB)
		if playerA.hero ~= nil or playerB.hero ~= nil then
			-- 按照血量进行排序
			return playerA.hero.hp > playerB.hero.hp
		else
			return playerA.name > playerB.name
		end
	end)
	return playerList
end

-- 当前存活玩家列表
function PlayerController.GetAlivePlayerList()
	local tmpList = {}
	for i, player in ipairs(playerList) do
		if player.hero.hp > 0 then
			table.insert(tmpList, player)
		end
	end
	return tmpList
end

-- 上阵棋子
function PlayerController.DeployPieces(player, pieces)
	if player:CheckPiecesListIsFree() then
		player:ReducePiecesFromCardsList(pieces)
		player:IncreasePiecesToPiecesList(pieces)
	end
end

-- 棋子回手牌
function PlayerController.RedeployPieces(player, pieces)
	if player:CheckCardsListIsFree() then
		player:ReducePiecesFromPiecesList(pieces)
		player:IncreasePiecesToCardsList(pieces)
	end
end

-- 棋子出售（回棋子池子）
function PlayerController.SellPieces(player, pieces)
	PiecesController.RecyclePiecesToPool(pieces.id)
	player:ReducePiecesFromPiecesList(pieces)
end

function PlayerController.Destroy()
	PlayerController.UnregisterEvents()
end

function PlayerController.PrintPiecesList(tag, piecesList)
    ChessUtil.Log("" .. tag)
    for i, pieces in ipairs(piecesList) do
        ChessUtil.Log("{Id: " .. pieces.id .. ", hp: " .. pieces.hp .. ", atk:" .. pieces.atk .. ", level:" .. pieces.level .. "}")
    end
end
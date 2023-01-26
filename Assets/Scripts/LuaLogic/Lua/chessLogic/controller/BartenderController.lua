local isInit = false
local bartenderList = {}

function BartenderController.Init()
	BartenderController.isInit = true
	ChessUtil.Log("BartenderController")
	BartenderController.RegisterEvents()

	-- 初始化当前酒馆等级的棋子阵容列表
	BartenderController.GenerateAllPlayersPiecesList()
	-- testChess
    ChessUtil.SendEvent(EVENT.CHESS_STAGE_BATTLE)
end

function BartenderController.RegisterEvents()
end

function BartenderController.UnregisterEvents()
end

function BartenderController.IsInit()
	return BartenderController.isInit
end

function BartenderController.GenerateAllPlayersPiecesList()
	local playerList = PlayerController.GetPlayerList()
	for i, player in ipairs(playerList) do
		player.bartender:GeneratePiecesList()
	end
end

-- 生成酒馆阵容列表
function BartenderController.GeneratePiecesList(bartenderLevel, maxLength)
	local piecesList = PiecesController.GetPiecesListFromPool(bartenderLevel, maxLength)
	return piecesList
end

-- 将酒馆的棋子放置回棋子池子
-- todoChess 调用场景：回合结束（将棋子放置回池子）、点击刷新酒馆（修改酒馆状态、将棋子放置回池子、生成新的棋子）
function BartenderController.RecyclePiecesList(piecesList)
	PiecesController.RecyclePiecesListToPool(piecesList)
end

-- 购买棋子
function BartenderController.BuyPieces(player, pieces)
	if player:CheckCurrencyIsEnough(pieces.cost) and player:CheckCardsListIsFree() and pieces:CheckStatusCanBuy() then
		PiecesController.GetPiecesFromPool(pieces.id)
		-- 玩家手牌列表更新
		player:IncreasePiecesToCardsList(pieces)
	end
end

-- 回收棋子
function BartenderController.SellPieces(player, pieces)
	if player:CheckPiecesListIsFree() then
		PiecesController.RecyclePiecesToPool(pieces.id)
		-- 玩家手牌列表更新
		player:ReducePiecesFromCardsList(pieces)
	end
end

-- 锁定/解锁棋子状态
function BartenderController.LockPieces(player, status)
	-- 设置酒馆状态
	player.bartender:SetBartenderStatus(status)
end

function BartenderController.Destroy()
	BartenderController.UnregisterEvents()
end
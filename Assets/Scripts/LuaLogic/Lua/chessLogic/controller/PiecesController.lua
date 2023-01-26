local isInit = false
-- 本局游戏的棋子池子
local piecesPool = nil
-- 本局游戏中的种族列表
local piecesRaceList = {}
-- 本局游戏中的移除种族列表
local piecesOutRaceList = {}

function PiecesController.Init()
	PiecesController.isInit = true
	ChessUtil.Log("PiecesController")
	PiecesController.RegisterEvents()

	PiecesController.GeneratePiecesRaceList()
	piecesPool = PiecesPool:new()
end

function PiecesController.RegisterEvents()
end

function PiecesController.UnregisterEvents()
end

function PiecesController.IsInit()
	return PiecesController.isInit
end

-- 取出指定等级和数量的阵容列表
function PiecesController.GetPiecesListFromPool(level, maxLength)
	local piecesList = {}
	-- 从棋子池子中获取对应棋子ID 随机列表
	local tmpPiecesIdList = ChessUtil.GetRandomList(piecesPool.idPool)
	-- 比较对应等级，获取到最大数量为止
	for i, piecesId in ipairs(tmpPiecesIdList) do
		-- 复制一份棋子阵容数据，不篡改原数据
		local pieces = clone(piecesPool.dataPool[piecesId])
		if pieces ~= nil and pieces.level <= level then
			if pieces.num > 0 then
				table.insert(piecesList, pieces)
				-- 减少池子中的棋子
				piecesPool:ReducePiecesNum(piecesId)
				if #piecesList >= maxLength then
					break
				end
			end
		end
	end
	return piecesList
end

-- 取出指定的一个棋子
function PiecesController.GetPiecesFromPool(piecesId)
	piecesPool:ReducePiecesNum(piecesId)
end

-- 回收棋子阵容列表
function PiecesController.RecyclePiecesListToPool(piecesList)
	if #piecesList > 0 then
		-- 将上一次进行锁操作的棋子释放
		for i, pieces in pairs(piecesList) do
			if pieces ~= nil then
				-- 增加池子中的棋子
				piecesPool:IncreasePiecesNum(pieces.id)
			end
		end
	end
end

-- 回收指定的一个棋子
function PiecesController.RecyclePiecesToPool(piecesId)
	piecesPool:IncreasePiecesNum(piecesId)
end

-- 随机本局游戏种族列表
function PiecesController.GeneratePiecesRaceList()
	-- 默认存在无种族
	piecesRaceList = {ChessConst.PiecesRaceType.None}
	piecesOutRaceList = {}

	local tmpList = {}
	for i, v in pairs(ChessConst.PiecesRaceType) do
		-- 去除默认种族
		for k, defaultRace in pairs(piecesRaceList) do
			if v ~= defaultRace then
				table.insert(tmpList, v)
			end
		end
	end
	tmpList = ChessUtil.GetRandomList(tmpList)

	-- m个不重复的数字中随机n个不重复的数字
	-- 将队列随机排序（遍历每一位，随机出要换的位置，进行交换），取前 n 个数字
	-- 从随机种族列表中获取战斗所需队列
	for i, v in ipairs(tmpList) do
		if i <= ChessConst.BattlePiecesRaceMax then
			table.insert(piecesRaceList, v)
		else
			table.insert(piecesOutRaceList, v)
		end
	end
end

function PiecesController.GetPiecesRaceList()
	return piecesRaceList
end

function PiecesController.Destroy()
	PiecesController.UnregisterEvents()
end
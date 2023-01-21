function PiecesPool:initialize()
    ChessUtil.Log("PiecesPool")

    -- 棋子池子
    self.idPool = {}
    self.dataPool = {}

	-- 根据本局游戏种族，生成本局游戏棋子池子
    self.piecesRaceList = PiecesController.GetPiecesRaceList()
    -- 将配表中的对用种族的棋子ID加入池子
    for piecesId, piecesData in pairs(ChessUtil.GetPiecesConfigData()) do
        for i, piecesRace in pairs(self.piecesRaceList) do
            if piecesData.race == piecesRace then
                table.insert(self.idPool, piecesId)
                self.dataPool[piecesId] = Pieces:new(piecesId)
            end
        end
    end
end

function PiecesPool:IncreasePiecesNum(piecesId)
    if self.dataPool[piecesId] ~= nil then
        self.dataPool[piecesId]:IncreaseNum()
    end
end

function PiecesPool:ReducePiecesNum(piecesId)
    if self.dataPool[piecesId] ~= nil then
        self.dataPool[piecesId]:ReduceNum()
    end
end

return PiecesPool
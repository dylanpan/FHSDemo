local isInit = false
local turn = 0
local battleResult = {}

function BattleController.Init()
    BattleController.isInit = true
	ChessUtil.Log("BattleController")
	BattleController.RegisterEvents()

    -- todoChess 玩家数量两两匹配进行战斗
    BattleController.StartBattle()
end

function BattleController.RegisterEvents()
end

function BattleController.UnregisterEvents()
end

function BattleController.IsInit()
    return BattleController.isInit
end

function BattleController.StartBattle()
	ChessUtil.Log("BattleController StartBattle")
    turn = turn + 1
    local battlePlayerList = BattleController.DividePlayerList()
    for i, vsGroup in ipairs(battlePlayerList) do
        -- todoChess 生成随机种子，用于回放战斗过程
	    ChessUtil.Log("BattleController StartBattle =========== start i: " .. i)
        local winPlayer, losePlayer, result = BattleController.PlayerBattle(false, vsGroup)
        if winPlayer ~= nil then
            PlayerController.PrintPiecesList("winPlayer: ", winPlayer.piecesList)
        end
        BattleController.SetBattleResult(result)
        ChessUtil.Log("BattleController StartBattle =========== end i: " .. i)
    end

    BattleController.EndBattle()
end

function BattleController.EndBattle()
	ChessUtil.Log("BattleController EndBattle")
    -- 判断结束游戏还是进入下一轮
    if #PlayerController.GetAlivePlayerList() > 1 then
        ChessUtil.SendEvent(EVENT.CHESS_STAGE_PICK_PIECES)
        print("chess - BattleController.BattleResult: " .. cjson.encode(battleResult))
    else
        turn = 0
        battleResult = {}
        ChessUtil.SendEvent(EVENT.CHESS_STAGE_END)
    end
end

-- todoChess 存储每一个轮次的各个战斗结果
function BattleController.SetBattleResult(result)
    if battleResult[turn .. ""] == nil then
        battleResult[turn .. ""] = {}
    end
    table.insert(battleResult[turn .. ""], result)
end

function BattleController.Destroy()
	BattleController.UnregisterEvents()
end

-- todoChess 对当前存活玩家进行两两分配，获取阵容
function BattleController.DividePlayerList()
    local playerList = PlayerController.GetPlayerList()
    -- todoChess 棋子权重初排序，强弱排序，强强排序，连输修正
    -- table.sort(playerList, function(playerA, playerB)
    --     -- 当前战力权重排序
    -- end)
    -- todoChess 列表排除掉血量为 0，且保持均有对手

    -- testChess 为玩家新增棋子
    for i, player in ipairs(playerList) do
        local piecesList = PiecesController.GetPiecesListFromPool(1, 3)
        for i, pieces in ipairs(piecesList) do
            PlayerController.DeployPieces(player, pieces)
        end 
    end

    local battlePlayerList = {
        {playerList[1], playerList[2]},
        {playerList[3], playerList[4]},
        {playerList[5], playerList[6]},
        {playerList[7], playerList[8]},
    }
    return battlePlayerList
end

-- todoChess playerA 和 playerB 进行战斗
function BattleController.PlayerBattle(isReplay, vsGroup, replayData)
    local result = isReplay and replayData or {}
    local battleStatus = true
    -- todoChess 战斗开始前处理: 英雄 Buff、棋子 Buff
    -- 随机先手
    local AFlag = isReplay and 0 or math.random(1, #vsGroup) == 1 and 1 or 2
    local BFlag = isReplay and 0 or AFlag == 1 and 2 or 1
    ChessUtil.Log("PlayerBattle: " .. AFlag .. " VS " .. BFlag .. " ==============")
    -- 复制一份棋子阵容数据，不篡改原数据
    local playerA = isReplay and replayData.playerA or clone(vsGroup[AFlag])
    local playerB = isReplay and replayData.playerB or clone(vsGroup[BFlag])
    if not isReplay then
        result.playerA = playerA:GetInfo()
        result.playerB = playerB:GetInfo()
    end
    local winPlayer = nil
    local _winPlayer = nil
    local losePlayer = nil
    local _losePlayer = nil
    -- todoChess 战斗开始中处理: 英雄 Buff、棋子 Buff
    local round = 0
    local battleIndex = 1
    while(battleStatus)
    do
        round = round + 1
        ChessUtil.Log("----------->>>>>")
        ChessUtil.Log("PlayerBattle round: " .. round)
        PlayerController.PrintPiecesList("PlayerBattle playerA: " .. playerA.name .. ", bartender.level: " .. playerA.bartender.level .. ", hp: " .. playerA.hero.hp, playerA.piecesList)
        PlayerController.PrintPiecesList("PlayerBattle playerB: " .. playerB.name .. ", bartender.level: " .. playerB.bartender.level .. ", hp: " .. playerB.hero.hp, playerB.piecesList)
        ChessUtil.Log("-----------")

        local atkPieces = playerA.piecesList[battleIndex]
        local defIndex = isReplay and replayData.battleIndex[round] or math.random(1, #playerB.piecesList)
        if not isReplay then
            if result.battleIndex == nil then
                result.battleIndex = {}
            end
            table.insert(result.battleIndex, defIndex)
        end
        local defPieces = playerB.piecesList[defIndex]
        BattleController.BattleIn(atkPieces, defPieces)

        -- 清除血量为 0 的棋子出列表
        if atkPieces.hp <= 0 then
            table.remove(playerA.piecesList, battleIndex)
        else
            battleIndex = battleIndex + 1
        end
        -- 棋子都攻击了一遍，重新开始
        if battleIndex > #playerA.piecesList then
            battleIndex = 1
        end
        if defPieces.hp <= 0 then
            table.remove(playerB.piecesList, defIndex)
        end

        -- 检查是否棋子全阵亡
        local playerAAllDead = #playerA.piecesList <= 0
        local playerBAllDead = #playerB.piecesList <= 0
        -- 设置结束标识
        if playerAAllDead then
            battleStatus = false
        elseif playerBAllDead then
            battleStatus = false
        end
        -- 设置获胜者
        if not battleStatus then
            if isReplay then
                winPlayer = replayData.playerA
                losePlayer = replayData.playerB
                if not playerAAllDead then
                    _winPlayer = playerA
                    _losePlayer = playerB
                elseif not playerBAllDead then
                    _winPlayer = playerB
                    _losePlayer = playerA
                end
            else
                if not playerAAllDead then
                    winPlayer = vsGroup[AFlag]
                    _winPlayer = playerA
                    losePlayer = vsGroup[BFlag]
                    _losePlayer = playerB
                elseif not playerBAllDead then
                    winPlayer = vsGroup[BFlag]
                    _winPlayer = playerB
                    losePlayer = vsGroup[AFlag]
                    _losePlayer = playerA
                end
            end
            PlayerController.PrintPiecesList("chess - final playerA: " .. playerA.name, playerA.piecesList)
            PlayerController.PrintPiecesList("chess - final playerB: " .. playerB.name, playerB.piecesList)
            ChessUtil.Log("-----------<<<<<")
        end
    end
    -- todoChess 战斗开始后处理: 英雄 Buff、棋子 Buff
    -- 战斗结果处理
    if winPlayer ~= nil then
        -- 扣除生命值
        local atk = _winPlayer.bartender.level
        for i, pieces in ipairs(_winPlayer.piecesList) do
            atk = atk + pieces.level
        end
        losePlayer.hero.hp = losePlayer.hero.hp - atk
        ChessUtil.Log("PlayerBattle winPlayer: " .. winPlayer.name .. ", bartender.level: " .. winPlayer.bartender.level .. ", hp: " .. winPlayer.hero.hp)
        ChessUtil.Log("PlayerBattle losePlayer: " .. losePlayer.name .. ", bartender.level: " .. losePlayer.bartender.level .. ", hp: " .. losePlayer.hero.hp)
    else
        ChessUtil.Log("PlayerBattle nooooo winPlayer")
    end
    return winPlayer, losePlayer, isReplay and replayData or result
end

-- 棋子对战，同时 piecesA 为先手，攻击一次
function BattleController.BattleIn(piecesA, piecesB)
    ChessUtil.Log("---->>")
    ChessUtil.Log("piecesA: " .. "{Id: " .. piecesA.id .. ", hp: " .. piecesA.hp .. ", atk:" .. piecesA.atk .. ", level:" .. piecesA.level .. "}")
    ChessUtil.Log("piecesB: " .. "{Id: " .. piecesB.id .. ", hp: " .. piecesB.hp .. ", atk:" .. piecesB.atk .. ", level:" .. piecesB.level .. "}")
    if piecesB.hp > 0 and piecesA.atk > 0 then
        piecesB.hp = piecesB.hp - piecesA.atk
        piecesA.hp = piecesA.hp - piecesB.atk
    end
    ChessUtil.Log("----")
    ChessUtil.Log("piecesA: " .. "{Id: " .. piecesA.id .. ", hp: " .. piecesA.hp .. ", atk:" .. piecesA.atk .. ", level:" .. piecesA.level .. "}")
    ChessUtil.Log("piecesB: " .. "{Id: " .. piecesB.id .. ", hp: " .. piecesB.hp .. ", atk:" .. piecesB.atk .. ", level:" .. piecesB.level .. "}")
    ChessUtil.Log("----<<")
end
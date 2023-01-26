function ChessUtil.Log(content)
    print("Chess - " .. content)
end

--事件注册
function ChessUtil.RegisterEvent(eventType, callback, instance, isCSharp)
    if isCSharp == nil then isCSharp = false end
    isCSharp = true

    if isCSharp then
        if instance ~= nil then
            MHEventMgr:Register(eventType, MHU3D.MHEventMgr.EventLuaHandler(callback, instance))
        else
            MHEventMgr:Register(eventType, MHU3D.MHEventMgr.EventLuaHandler(callback))
        end
    else
        EventSystem:RegisterEvent(eventType, callback, instance)
    end
end

--事件注销
function ChessUtil.UnregisterEvent(eventType, callback, instance, isCSharp)
    if isCSharp == nil then isCSharp = false end
    isCSharp = true

    if isCSharp then
        if instance ~= nil then
            MHEventMgr:Unregister(eventType, MHU3D.MHEventMgr.EventLuaHandler(callback, instance))
        else
            MHEventMgr:Unregister(eventType, MHU3D.MHEventMgr.EventLuaHandler(callback))
        end
    else
        EventSystem:UnregisterEvent(eventType, callback, instance)
    end
end

function ChessUtil.SendEvent(eventType, param, isCSharp)
	if isCSharp == nil then isCSharp = false end
	isCSharp = true

	if isCSharp then
	    if param == nil then
            MHEventMgr:SendEvent(eventType)
        else
            MHEventMgr:SendEvent(eventType, param)
        end
    else
        EventSystem:SendEvent(eventType, param)
    end
end

-- 将队列随机排序（遍历每一位，随机出要换的位置，进行交换）
function ChessUtil.GetRandomList(list)
    if #list > 0 then
        for i, v in pairs(list) do
            local randomI = math.random(1, #list)
            if randomI ~= i then
                local tmpV = v
                local randomV = list[randomI]
                table.remove(list, randomI)
                if randomI > #list then
                    table.insert(list, tmpV)
                else
                    table.insert(list, randomI, tmpV)
                end
                table.remove(list, i)
                if i > #list then
                    table.insert(list, randomV)
                else
                    table.insert(list, i, randomV)
                end
            end
        end
    else
        ChessUtil.Log("Util GetRandomList list Length: 0")
    end
    return list
end

-- testChess 测试数据表
function ChessUtil.GetPlayerConfigData()
    return {
        currency = 3,
        piecesListLength = 7,
        cardsListLength = 10
    }
end

function ChessUtil.GetPiecesConfigData()
    return {
        [1000] = {id = 1000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.None, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 1, atk = 1, skillId = 10001},
        [1001] = {id = 1001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.None, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 10011},
        [1002] = {id = 1002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.None, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 10021},
        [1003] = {id = 1003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.None, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 10031},
        [2000] = {id = 2000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.Fish, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 2, atk = 2, skillId = 20001},
        [2001] = {id = 2001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.Fish, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 20011},
        [2002] = {id = 2002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.Fish, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 20021},
        [2003] = {id = 2003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.Fish, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 20031},
        [3000] = {id = 3000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.Pig, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 3, atk = 1, skillId = 30001},
        [3001] = {id = 3001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.Pig, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 30011},
        [3002] = {id = 3002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.Pig, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 30021},
        [3003] = {id = 3003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.Pig, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 30031},
        [4000] = {id = 4000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.Demon, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 1, atk = 4, skillId = 40001},
        [4001] = {id = 4001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.Demon, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 40011},
        [4002] = {id = 4002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.Demon, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 40021},
        [4003] = {id = 4003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.Demon, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 40031},
        [5000] = {id = 5000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.Machine, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 2, atk = 3, skillId = 50001},
        [5001] = {id = 5001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.Machine, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 50011},
        [5002] = {id = 5002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.Machine, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 50021},
        [5003] = {id = 5003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.Machine, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 50031},
        [6000] = {id = 6000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.Pirate, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 3, atk = 2, skillId = 60001},
        [6001] = {id = 6001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.Pirate, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 60011},
        [6002] = {id = 6002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.Pirate, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 60021},
        [6003] = {id = 6003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.Pirate, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 60031},
        [7000] = {id = 7000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.Dragon, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 5, atk = 0, skillId = 70001},
        [7001] = {id = 7001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.Dragon, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 70011},
        [7002] = {id = 7002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.Dragon, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 70021},
        [7003] = {id = 7003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.Dragon, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 70031},
        [8000] = {id = 8000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.Beast, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 2, atk = 1, skillId = 80001},
        [8001] = {id = 8001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.Beast, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 80011},
        [8002] = {id = 8002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.Beast, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 80021},
        [8003] = {id = 8003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.Beast, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 80031},
        [9000] = {id = 9000, name = "", skin = "", level = 1, race = ChessConst.PiecesRaceType.Element, cost = 3, num = 27, maxNum = 27, receive = 1, hp = 1, atk = 1, skillId = 90001},
        [9001] = {id = 9001, name = "", skin = "", level = 2, race = ChessConst.PiecesRaceType.Element, cost = 3, num = 21, maxNum = 21, receive = 1, hp = 1, atk = 1, skillId = 90011},
        [9002] = {id = 9002, name = "", skin = "", level = 3, race = ChessConst.PiecesRaceType.Element, cost = 3, num = 18, maxNum = 18, receive = 1, hp = 1, atk = 1, skillId = 90021},
        [9003] = {id = 9003, name = "", skin = "", level = 4, race = ChessConst.PiecesRaceType.Element, cost = 3, num = 12, maxNum = 12, receive = 1, hp = 1, atk = 1, skillId = 90031},
    }
end

function ChessUtil.GetPiecesSkillConfigData()
    return {
        [10001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {1,1}},
        [10011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {1,1}},
        [10021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {1,1}},
        [10031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {1,1}},
        [20001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {2,2}},
        [20011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {2,2}},
        [20021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {2,2}},
        [20031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {2,2}},
        [30001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {3,3}},
        [30011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {3,3}},
        [30021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {3,3}},
        [30031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {3,3}},
        [40001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {4,4}},
        [40011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {4,4}},
        [40021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {4,4}},
        [40031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {4,4}},
        [50001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {5,5}},
        [50011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {5,5}},
        [50021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {5,5}},
        [50031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {5,5}},
        [60001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {6,6}},
        [60011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {6,6}},
        [60021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {6,6}},
        [60031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {6,6}},
        [70001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {7,7}},
        [70011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {7,7}},
        [70021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {7,7}},
        [70031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {7,7}},
        [80001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {8,8}},
        [80011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {8,8}},
        [80021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {8,8}},
        [80031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {8,8}},
        [90001] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {9,9}},
        [90011] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {9,9}},
        [90021] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {9,9}},
        [90031] = {name = "", skin = "", type = ChessConst.PiecesSkillType.BattleCry, buffIdList = {9,9}},
    }
end

function ChessUtil.GetHeroConfigData()
    return {
        [10] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 101},
        [11] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 101},
        [12] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 101},
        [13] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 101},
        [20] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 201},
        [21] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 201},
        [22] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 201},
        [23] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 201},
        [30] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 301},
        [31] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 301},
        [32] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 301},
        [33] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 301},
        [40] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 401},
        [41] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 401},
        [42] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 401},
        [43] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 401},
        [50] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 501},
        [51] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 501},
        [52] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 501},
        [53] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 501},
        [60] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 601},
        [61] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 601},
        [62] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 601},
        [63] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 601},
        [70] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 701},
        [71] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 701},
        [72] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 701},
        [73] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 701},
        [80] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 801},
        [81] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 801},
        [82] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 801},
        [83] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 801},
        [90] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 901},
        [91] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 901},
        [92] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 901},
        [93] = {name = "", skin = "", hp = 40, atk = 0, num = 1, skillId = 901},
    }
end

function ChessUtil.GetHeroSkillConfigData()
    return {
        [101] = {name = "", skin = "", cost = 0, type = ChessConst.HeroSkillType.Active, buffIdList = {1,1}},
        [201] = {name = "", skin = "", cost = 1, type = ChessConst.HeroSkillType.Active, buffIdList = {1,1}},
        [301] = {name = "", skin = "", cost = 1, type = ChessConst.HeroSkillType.Active, buffIdList = {1,1}},
        [401] = {name = "", skin = "", cost = 1, type = ChessConst.HeroSkillType.Active, buffIdList = {1,1}},
        [501] = {name = "", skin = "", cost = 1, type = ChessConst.HeroSkillType.Active, buffIdList = {1,1}},
        [601] = {name = "", skin = "", cost = 1, type = ChessConst.HeroSkillType.Active, buffIdList = {1,1}},
        [701] = {name = "", skin = "", cost = 1, type = ChessConst.HeroSkillType.Active, buffIdList = {1,1}},
        [801] = {name = "", skin = "", cost = 2, type = ChessConst.HeroSkillType.Active, buffIdList = {10,10}},
        [901] = {name = "", skin = "", cost = 3, type = ChessConst.HeroSkillType.Active, buffIdList = {11,11}},
    }
end

function ChessUtil.GetBartenderLevelConfigData()
    return {
        [1] = {cost = 0, piecesListLength = 3},
        [2] = {cost = 5, piecesListLength = 4},
        [3] = {cost = 6, piecesListLength = 4},
        [4] = {cost = 7, piecesListLength = 5},
        [5] = {cost = 8, piecesListLength = 5},
        [6] = {cost = 9, piecesListLength = 6},
    }
end

function ChessUtil.GetBartenderSkinConfigData()
    return {
        [1] = {name = "", skin = ""},
        [2] = {name = "", skin = ""},
    }
end

function ChessUtil.GetAtkBuffConfigData()
    return {
        [1] = {add = 2, raceList = {}, num = 1},
        [2] = {add = 1, raceList = {ChessConst.PiecesRaceType.Fish}, num = ChessConst.BattlePiecesMax},
        [3] = {add = 1, raceList = {ChessConst.PiecesRaceType.Pig}, num = ChessConst.BattlePiecesMax},
        [4] = {add = 1, raceList = {ChessConst.PiecesRaceType.Demon}, num = ChessConst.BattlePiecesMax},
        [5] = {add = 1, raceList = {ChessConst.PiecesRaceType.Machine}, num = ChessConst.BattlePiecesMax},
        [6] = {add = 1, raceList = {ChessConst.PiecesRaceType.Pirate}, num = ChessConst.BattlePiecesMax},
        [7] = {add = 1, raceList = {ChessConst.PiecesRaceType.Dragon}, num = ChessConst.BattlePiecesMax},
        [8] = {add = 1, raceList = {ChessConst.PiecesRaceType.Beast}, num = ChessConst.BattlePiecesMax},
        [9] = {add = 1, raceList = {ChessConst.PiecesRaceType.Element}, num = ChessConst.BattlePiecesMax},
        [10] = {add = 1, raceList = {}, num = 3},
        [11] = {add = 2, raceList = {}, num = 3},
    }
end

function ChessUtil.GetHpBuffConfigData()
    return {
        [1] = {add = 2, raceList = {}, num = 1},
        [2] = {add = 1, raceList = {ChessConst.PiecesRaceType.Fish}, num = ChessConst.BattlePiecesMax},
        [3] = {add = 1, raceList = {ChessConst.PiecesRaceType.Pig}, num = ChessConst.BattlePiecesMax},
        [4] = {add = 1, raceList = {ChessConst.PiecesRaceType.Demon}, num = ChessConst.BattlePiecesMax},
        [5] = {add = 1, raceList = {ChessConst.PiecesRaceType.Machine}, num = ChessConst.BattlePiecesMax},
        [6] = {add = 1, raceList = {ChessConst.PiecesRaceType.Pirate}, num = ChessConst.BattlePiecesMax},
        [7] = {add = 1, raceList = {ChessConst.PiecesRaceType.Dragon}, num = ChessConst.BattlePiecesMax},
        [8] = {add = 1, raceList = {ChessConst.PiecesRaceType.Beast}, num = ChessConst.BattlePiecesMax},
        [9] = {add = 1, raceList = {ChessConst.PiecesRaceType.Element}, num = ChessConst.BattlePiecesMax},
        [10] = {add = 1, raceList = {}, num = 3},
        [11] = {add = 2, raceList = {}, num = 3},
    }
end


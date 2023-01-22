using System;

public class BattleAutoChessSystem : ISystem
{
    public Entity CreateBattleResultEntity(Entity entity_a, Entity entity_b)
    {
        Entity entity = new Entity();
        entity.AddComponent(new NameComponent(){name = "BattleResult"});
        ResultComponent resultComponent = new ResultComponent();
        PiecesListComponent aPiecesListComponent = (PiecesListComponent)entity_a.GetComponent<PiecesListComponent>();
        PiecesListComponent bPiecesListComponent = (PiecesListComponent)entity_b.GetComponent<PiecesListComponent>();
        List<Entity> aresultPiecesList = GetBattlePiecesEntityList(aPiecesListComponent.piecesIds);
        List<Entity> bresultPiecesList = GetBattlePiecesEntityList(bPiecesListComponent.piecesIds);
        Dictionary<int, List<Entity>> resultPiecesDict = new Dictionary<int, List<Entity>>();
        // 0 - a 队，1 - b 队
        resultPiecesDict.Add(0, aresultPiecesList);
        resultPiecesDict.Add(1, bresultPiecesList);
        // 字典 key 为轮次
        resultComponent.result_dict.Add(0, resultPiecesDict);
        entity.AddComponent(resultComponent);
        return entity;
    }
    
    // 将 ID 对应的复制 Entity 一个新的出来
    public List<Entity> GetBattlePiecesEntityList(List<int> piecesIds)
    {
        List<Entity> piecesList = new List<Entity>();
        foreach (int piecesId in piecesIds)
        {
            Entity copyEntity = Util.CopyEntity(piecesId);
            piecesList.Add(copyEntity);
        }
        return piecesList;
    }
    public void GetBattleResult(int round, ref Entity entity)
    {
        ResultComponent resultComponent = (ResultComponent)entity.GetComponent<ResultComponent>();
        List<Entity> aList = resultComponent.result_dict[round-1][0];
        List<Entity> bList = resultComponent.result_dict[round-1][1];
        if (resultComponent != null)
        {
            if (Util.Battle_CheckListAllStatus(3, aList) && Util.Battle_CheckListAllStatus(3, bList))
            {
                // 结束：同空，平局
                resultComponent.status = 1;
            }
            else if (!Util.Battle_CheckListAllStatus(3, aList) && Util.Battle_CheckListAllStatus(3, bList))
            {
                // 结束：b 空，a 赢
                resultComponent.status = 2;
            }
            else if (Util.Battle_CheckListAllStatus(3, aList) && !Util.Battle_CheckListAllStatus(3, bList))
            {
                // 结束：a 空， b 赢
                resultComponent.status = 3;
            }
            else
            {
                // 未结束：走当前轮次
                resultComponent.status = 0;
                int b = round % 2;
                int a = 1 - b;
                // TODO: 目前为了查看日志方便进行了每一个轮次的复制,后续和展示层关联应该还是会使用同一个,需要考虑回放日志,制作战斗回放数据
                List<Entity> _aList = Util.CopyList(resultComponent.result_dict[round-1][a]);
                List<Entity> _bList = Util.CopyList(resultComponent.result_dict[round-1][b]);
                Dictionary<int, List<Entity>> tmpDict = DoBattle(_aList, _bList);
                if (!resultComponent.result_dict.ContainsKey(round))
                {
                    resultComponent.result_dict.Add(round, new Dictionary<int, List<Entity>>());
                }
                // 当前轮次战斗结果
                resultComponent.result_dict[round][a] = tmpDict[0];
                resultComponent.result_dict[round][b] = tmpDict[1];
            }
            if (resultComponent.status == 0)
            {
                round = round + 1;
                GetBattleResult(round, ref entity);
            }
        }
    }
    public Dictionary<int, List<Entity>> DoBattle(List<Entity> aList, List<Entity> bList)
    {
        Dictionary<int, List<Entity>> tmpDict = new Dictionary<int, List<Entity>>();
        // 找到对应的 a 和 b 的 pieces entity
        int findAtkIndex = -1;
        Entity atkEntity = Util.Battle_FindAtkEntity(aList, out findAtkIndex);
        Entity defEntity = Util.Battle_FindDefEntity(bList);

        // 进行 a - b
        if (atkEntity != null && defEntity != null)
        {
            PorpertyComponent atkPorpertyComponent = (PorpertyComponent)atkEntity.GetComponent<PorpertyComponent>();
            PorpertyComponent defPorpertyComponent = (PorpertyComponent)defEntity.GetComponent<PorpertyComponent>();
            if (atkPorpertyComponent.hp > 0 && defPorpertyComponent.hp > 0)
            {
                defPorpertyComponent.hp -= atkPorpertyComponent.atk;
                atkPorpertyComponent.hp -= defPorpertyComponent.atk;
            }

            // 将 hp = 0 的更新状态
            if (atkPorpertyComponent.hp <= 0)
            {
                Util.Battle_SetEntityStatus(atkEntity, 3);
            }
            if (defPorpertyComponent.hp <= 0)
            {
                Util.Battle_SetEntityStatus(defEntity, 3);
            }

            // 转换攻击 Index
            if (aList.Count > 0)
            {
                Util.Battle_SetEntityStatus(atkEntity, -1);
                int findNextAtkIndex = -1;
                Entity nextAtkEntity = Util.Battle_FindAtkEntity(aList, out findNextAtkIndex);
                if (nextAtkEntity != null)
                {
                    Util.Battle_SetEntityStatus(nextAtkEntity, 2);
                }
                
                if (bList.Count > 0)
                {
                    int findOtherAtkIndex = -1;
                    Entity otherAtkEntity = Util.Battle_FindAtkEntity(bList, out findOtherAtkIndex);
                    if (otherAtkEntity != null)
                    {
                        Util.Battle_SetEntityStatus(otherAtkEntity, 2);
                    }
                }
            }
        }
        tmpDict.Add(0, aList);
        tmpDict.Add(1, bList);
        return tmpDict;
    }
    public override void Update()
    {
        Console.WriteLine("BattleAutoChessSystem Update");
        
        List<Entity> battleEntitys = new List<Entity>();
        for (int i = 0; i < World.Instance.entityDic.Values.Count; i++)
        {
            Entity entity = World.Instance.entityDic.Values.ElementAt(i);
            PiecesListComponent piecesListComponent = (PiecesListComponent)entity.GetComponent<PiecesListComponent>();
            if (piecesListComponent != null && piecesListComponent.battle_card_id != -1)
            {
                battleEntitys.Add(entity);
            }
        }
        Entity battleAEntity = battleEntitys.Count > 0 ? battleEntitys[0] : Util.Battle_GetEmptyEntity();
        Entity battleBEntity = battleEntitys.Count > 1 ? battleEntitys[1] : Util.Battle_GetEmptyEntity();
        TestUtil.SetTestPiecesIds(ref battleAEntity, ref battleBEntity);
        Entity resultEntity = CreateBattleResultEntity(battleAEntity, battleBEntity);
        GetBattleResult(1, ref resultEntity);
        World.Instance.AddEntity(resultEntity);
    }
}
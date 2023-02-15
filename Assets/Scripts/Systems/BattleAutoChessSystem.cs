using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Config;
using Chess.Component;
using Chess.Util;
using UnityEngine;

namespace Chess.Systems
{
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
            // 字典 key 为轮次，从 0 开始
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
                Entity copyEntity = CommonUtil.CopyEntity(piecesId);
                piecesList.Add(copyEntity);
            }
            return piecesList;
        }
        public void GetBattleResult(int round, ref Entity entity)
        {
            ResultComponent resultComponent = (ResultComponent)entity.GetComponent<ResultComponent>();
            List<Entity> aList = resultComponent.result_dict[round-1][ConstUtil.Team_A];
            List<Entity> bList = resultComponent.result_dict[round-1][ConstUtil.Team_B];
            if (resultComponent != null)
            {
                if (aList.Count <= 0 && bList.Count <= 0)
                {
            Debug.Log("BattleAutoChessSystem DoBattle-----------------01");

                }
                else if (aList.Count > 0 && bList.Count <= 0)
                {
            Debug.Log("BattleAutoChessSystem DoBattle-----------------02");

                }
                else if (aList.Count <= 0 && bList.Count > 0)
                {
            Debug.Log("BattleAutoChessSystem DoBattle-----------------03");

                }
                else if ((CommonUtil.Battle_CheckListAllSpecificStatus(new int[]{ConstUtil.Status_Piece_Dead,ConstUtil.Status_Piece_No_Atk}, aList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.None, aList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.Status_Piece_Atk, aList))
                    && (CommonUtil.Battle_CheckListAllSpecificStatus(new int[]{ConstUtil.Status_Piece_Dead,ConstUtil.Status_Piece_No_Atk}, bList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.None, bList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.Status_Piece_Atk, bList)))
                {
                    // 结束：同空，平局
                    resultComponent.status = ConstUtil.Result_Status_Draw;
                }
                else if ((!CommonUtil.Battle_CheckListAllSpecificStatus(new int[]{ConstUtil.Status_Piece_Dead,ConstUtil.Status_Piece_No_Atk}, aList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.None, aList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.Status_Piece_Atk, aList))
                        && (CommonUtil.Battle_CheckListAllSpecificStatus(new int[]{ConstUtil.Status_Piece_Dead,ConstUtil.Status_Piece_No_Atk}, bList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.None, bList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.Status_Piece_Atk, bList)))
                {
                    // 结束：b 空，a 赢
                    resultComponent.status = ConstUtil.Result_Status_Win_A;
                }
                else if ((CommonUtil.Battle_CheckListAllSpecificStatus(new int[]{ConstUtil.Status_Piece_Dead,ConstUtil.Status_Piece_No_Atk}, aList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.None, aList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.Status_Piece_Atk, aList))
                        && (!CommonUtil.Battle_CheckListAllSpecificStatus(new int[]{ConstUtil.Status_Piece_Dead,ConstUtil.Status_Piece_No_Atk}, bList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.None, bList) && !CommonUtil.Battle_CheckListHaveStatus(ConstUtil.Status_Piece_Atk, bList)))
                {
                    // 结束：a 空， b 赢
                    resultComponent.status = ConstUtil.Result_Status_Win_B;
                }
                else
                {
                    // 未结束：走当前轮次
                    resultComponent.status = ConstUtil.Result_Status_Unknown;
                    int b = round % 2;
                    int a = 1 - b;
                    // TODO: 目前为了查看日志方便进行了每一个轮次的复制,后续和展示层关联应该还是会使用同一个,需要考虑回放日志,制作战斗回放数据
                    // TODO: 战斗辅助工具:1.支持配置队伍信息;2.支持手动回合战斗;3.通过数据进行回放;
                    List<Entity> _curRoundAList = CommonUtil.CopyList(resultComponent.result_dict[round-1][a]);
                    List<Entity> _curRoundBList = CommonUtil.CopyList(resultComponent.result_dict[round-1][b]);
                    Dictionary<int, List<Entity>> tmpDict = DoBattle(_curRoundAList, _curRoundBList);
                    if (!resultComponent.result_dict.ContainsKey(round))
                    {
                        resultComponent.result_dict.Add(round, new Dictionary<int, List<Entity>>());
                    }
                    // 当前轮次战斗结果
                    resultComponent.result_dict[round][a] = tmpDict[ConstUtil.Team_A];
                    resultComponent.result_dict[round][b] = tmpDict[ConstUtil.Team_B];
                }
                if (resultComponent.status == ConstUtil.Result_Status_Unknown)
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
            Entity atkEntity = CommonUtil.Battle_FindAtkEntity(aList, out findAtkIndex);
            Entity defEntity = CommonUtil.Battle_FindDefEntity(bList);

            // 进行 a - b
            if (atkEntity != null && defEntity == null)
            {
                // 无 Def 表示已经结束
                for (int i = 0; i < aList.Count; i++)
                {
                    Entity entity = aList[i];
                    int status = CommonUtil.Battle_GetEntityStatus(entity);
                    if (status != ConstUtil.Status_Piece_Dead || status != ConstUtil.Status_Piece_No_Atk)
                    {
                        CommonUtil.Battle_UpdateEntityStatus(entity, true);
                    }
                }
            }
            else if (atkEntity == null && defEntity != null)
            {
                // 无 Def 表示已经结束
                for (int i = 0; i < bList.Count; i++)
                {
                    Entity entity = bList[i];
                    int status = CommonUtil.Battle_GetEntityStatus(entity);
                    if (status != ConstUtil.Status_Piece_Dead || status != ConstUtil.Status_Piece_No_Atk)
                    {
                        CommonUtil.Battle_UpdateEntityStatus(entity, true);
                    }
                }
            }
            else if (atkEntity != null && defEntity != null)
            {
                PorpertyComponent atkPorpertyComponent = (PorpertyComponent)atkEntity.GetComponent<PorpertyComponent>();
                PorpertyComponent defPorpertyComponent = (PorpertyComponent)defEntity.GetComponent<PorpertyComponent>();
                if (atkPorpertyComponent.hp > 0 && defPorpertyComponent.hp > 0 && atkPorpertyComponent.atk > 0)
                {
                    defPorpertyComponent.hp -= atkPorpertyComponent.atk;
                    atkPorpertyComponent.hp -= defPorpertyComponent.atk;
                }

                CommonUtil.Battle_UpdateEntityStatus(atkEntity);
                CommonUtil.Battle_UpdateEntityStatus(defEntity);

                // 转换攻击 Index
                if (aList.Count > 0)
                {
                    int findNextAtkIndex = -1;
                    Entity nextAtkEntity = CommonUtil.Battle_FindAtkEntity(aList, out findNextAtkIndex);
                }
                if (bList.Count > 0)
                {
                    int findOtherAtkIndex = -1;
                    Entity otherAtkEntity = CommonUtil.Battle_FindAtkEntity(bList, out findOtherAtkIndex);
                }
            }
            tmpDict.Add(ConstUtil.Team_A, aList);
            tmpDict.Add(ConstUtil.Team_B, bList);
            return tmpDict;
        }
        public override void Update()
        {
            List<int> player_list = Process.GetInstance().GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Battle_Start))
                {
                    Debug.Log("BattleAutoChessSystem Update - init");
                    Entity player = World.Instance.entityDic[player_id];
                    List<Entity> battleEntitys = new List<Entity>();
                    battleEntitys.Add(player);
                    PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                    if (playerComponent != null)
                    {
                        Entity rival = World.Instance.entityDic[playerComponent.rival_id];
                        battleEntitys.Add(rival);

                        Entity battleAEntity = battleEntitys.Count > 0 ? battleEntitys[ConstUtil.Team_A] : CommonUtil.Battle_GetEmptyEntity();
                        Entity battleBEntity = battleEntitys.Count > 1 ? battleEntitys[ConstUtil.Team_B] : CommonUtil.Battle_GetEmptyEntity();
                        Entity resultEntity = CommonUtil.RandomFirstIndex(1) == 0 ? CreateBattleResultEntity(battleAEntity, battleBEntity) : CreateBattleResultEntity(battleBEntity, battleAEntity);
                        GetBattleResult(1, ref resultEntity);
                        World.Instance.AddEntity(resultEntity);
                        Process.GetInstance().SetProcess(ConstUtil.Process_Battle_End, player_id);
                        Process.GetInstance().SetProcess(ConstUtil.Process_Battle_End, rival.ID);
                    }
                }
            }
        }
    }
}
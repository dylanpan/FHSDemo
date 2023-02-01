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
    public class PiecesPoolSystem : ISystem
    {
        public void GeneratePieceEntity(PiecesConfig piecesConfig)
        {
            if (piecesConfig != null)
            {
                for (int i = 0; i < piecesConfig.piece_num; i++)
                {
                    Entity entity = new Entity();
                    entity.AddComponent(new NameComponent(){name = piecesConfig.name, id = piecesConfig.id});
                    entity.AddComponent(new SkinComponent(){skin_name = piecesConfig.skin_name});
                    entity.AddComponent(new LevelComponent(){level = piecesConfig.level});
                    entity.AddComponent(new CurrencyComponent(){piece_cost = piecesConfig.piece_cost, piece_recycle = piecesConfig.piece_recycle});
                    entity.AddComponent(new PorpertyComponent(){atk = piecesConfig.atk, hp = piecesConfig.hp, race = piecesConfig.race});
                    entity.AddComponent(new BuffComponent());
                    entity.AddComponent(new StatusComponent());
                    World.Instance.AddEntity(entity);
                    Process.Instance.AddPiecePoolToDict(piecesConfig.level, entity.ID);
                }
            }
        }

        public void GeneratePoolFormConfig()
        {
            List<PiecesConfig> configDataList = ConfigUtil.GetConfigDataList<PiecesConfig>(ConstUtil.Json_File_Pieces_Config);
            if (configDataList.Count > 0)
            {
                for (int i = 0; i < configDataList.Count; i++)
                {
                    PiecesConfig piecesConfig = configDataList[i];
                    GeneratePieceEntity(piecesConfig);
                }
            }
            else
            {
                Debug.Log("PiecesPoolSystem get empty config");
            }
        }
        public List<int> GetRamdomPiecesFormPool(int max)
        {
            List<int> piece_pool = new List<int>();
            for (int i = 0; i < max; i++)
            {
                piece_pool.Add(RandomPiece());
            }
            return piece_pool;
        }
        private int RandomPiece()
        {
            int id = ConstUtil.None;
            Entity player = World.Instance.entityDic[Process.Instance.GetSelfPlayerId()];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    Entity bartender = World.Instance.entityDic[playerComponent.bartender_id];
                    if (bartender != null)
                    {
                        LevelComponent levelComponent = (LevelComponent)bartender.GetComponent<LevelComponent>();
                        if (levelComponent != null)
                        {
                            List<int> piece_pool = Process.Instance.GetPiecePoolFormDict(levelComponent.level);
                            if (piece_pool.Count > 0)
                            {
                                while (true)
                                {
                                    id = piece_pool[CommonUtil.RandomPiecesIndex(piece_pool.Count)];
                                    Entity piece = World.Instance.entityDic[id];
                                    if (CommonUtil.Battle_GetEntityStatus(piece) != ConstUtil.Status_Piece_Pick
                                        && CommonUtil.Battle_GetEntityStatus(piece) != ConstUtil.Status_Piece_Freeze)
                                    {
                                        CommonUtil.Battle_SetEntityStatus(piece, ConstUtil.Status_Piece_Pick);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return id;
        }

        private PiecesListComponent UpdateBartenderPiecesList(out int bartender_id)
        {
            bartender_id = ConstUtil.None;
            PiecesListComponent piecesListComponent;
            Entity player = World.Instance.entityDic[Process.Instance.GetSelfPlayerId()];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    if (playerComponent.bartender_id != ConstUtil.None)
                    {
                        bartender_id = playerComponent.bartender_id;
                        Entity bartender = World.Instance.entityDic[playerComponent.bartender_id];
                        piecesListComponent = (PiecesListComponent)bartender.GetComponent<PiecesListComponent>();
                    }
                }
            }
            return piecesListComponent;
        }

        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Heroes_Pool)
            {
                Debug.Log("PiecesPoolSystem Update - init");
                GeneratePoolFormConfig();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Pieces_Pool);
            }
            else if (Process.Instance.GetProcess() == ConstUtil.Process_Prepare_Start)
            {
                // 从池子中抽取当前玩家酒馆的棋子信息
                int bartender_id = ConstUtil.None;
                PiecesListComponent piecesListComponent = UpdateBartenderPiecesList(out bartender_id);
                if (piecesListComponent != null)
                {
                    if (piecesListComponent.piecesIds.Count <= 0)
                    {
                        Debug.Log("PiecesPoolSystem Update - prepare start");
                        piecesListComponent.piecesIds = GetRamdomPiecesFormPool(piecesListComponent.max_num);
                        EventUtil.Instance.SendEvent(ConstUtil.Event_Type_update_bartender_pieces_view, bartender_id);
                    }
                    else
                    {
                        // TODO: - 1 承接上回合结束，刷新酒馆已有非冻结棋子 GetRamdomPiecesFormPool(计算num有几个)
                    }
                }
            }
            else if (Process.Instance.GetProcess() == ConstUtil.Process_Prepare_Bartender_Refresh)
            {
                int bartender_id = ConstUtil.None;
                PiecesListComponent piecesListComponent = UpdateBartenderPiecesList(out bartender_id);
                if (piecesListComponent != null)
                {
                    // TODO: - 1 刷新酒馆已有非冻结棋子 GetRamdomPiecesFormPool(计算num有几个)
                }
            }
            else if (Process.Instance.GetProcess() == ConstUtil.Process_Prepare_End)
            {
                // TODO: 回合结束的冻结的棋子
            }
        }
    }
}
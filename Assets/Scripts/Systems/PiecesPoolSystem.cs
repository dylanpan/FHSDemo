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
            List<PiecesConfig> configDataList = ConfigUtil.GetConfigData<PiecesConfig>(ConstUtil.Json_File_Pieces_Config);
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
            List<int> piece_pool = Process.Instance.GetPiecePoolFormDict(Process.Instance.GetCurrentLevel());
            int id = ConstUtil.None;
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
            return id;
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
                // TODO: 设置当前回合Buff信息(考虑防止在BuffSys中): 对应玩家手牌和场上的棋子信息和各个酒馆的棋子信息
                // 从池子中抽取当前玩家酒馆的棋子信息
                if (Process.Instance.GetBartenderId() != ConstUtil.None)
                {
                    Entity bartender = World.Instance.entityDic[Process.Instance.GetBartenderId()];
                    PiecesListComponent piecesListComponent = (PiecesListComponent)bartender.GetComponent<PiecesListComponent>();
                    if (piecesListComponent.piecesIds.Count <= 0)
                    {
                        Debug.Log("PiecesPoolSystem Update - battle prepare");
                        piecesListComponent.piecesIds = GetRamdomPiecesFormPool(piecesListComponent.max_num);
                        // TODO: 需要补充通知更新页面棋子信息
                    }
                }
            }
        }
    }
}
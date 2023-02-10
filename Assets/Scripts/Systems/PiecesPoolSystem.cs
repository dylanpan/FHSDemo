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
                    entity.AddComponent(new NameComponent(){name = piecesConfig.name, id = piecesConfig.id, belong = ConstUtil.Belong_Pool});
                    entity.AddComponent(new SkinComponent(){skin_name = piecesConfig.skin_name});
                    entity.AddComponent(new LevelComponent(){level = piecesConfig.level});
                    entity.AddComponent(new CurrencyComponent(){piece_cost = piecesConfig.piece_cost, piece_recycle = piecesConfig.piece_recycle});
                    entity.AddComponent(new PorpertyComponent(){atk = piecesConfig.atk, hp = piecesConfig.hp, race = piecesConfig.race});
                    entity.AddComponent(new BuffComponent());
                    entity.AddComponent(new StatusComponent());
                    entity.AddComponent(new ConfigComponent<PiecesConfig>(){config = piecesConfig});
                    World.Instance.AddEntity(entity);
                    Process.GetInstance().AddPiecePoolToDict(piecesConfig.level, entity.ID);
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
        public List<int> GetRamdomPiecesFormPool(int player_id, int max)
        {
            List<int> piece_pool = new List<int>();
            if (max > 0)
            {
                for (int i = 0; i < max; i++)
                {
                    piece_pool.Add(RandomPiece(player_id));
                }
            }
            return piece_pool;
        }
        private int RandomPiece(int player_id)
        {
            int id = ConstUtil.None;
            Entity player = World.Instance.entityDic[player_id];
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
                            List<int> piece_pool = Process.GetInstance().GetPiecePoolFormDict(levelComponent.level);
                            if (piece_pool.Count > 0)
                            {
                                while (true)
                                {
                                    id = piece_pool[CommonUtil.RandomPiecesIndex(piece_pool.Count)];
                                    Entity piece = World.Instance.entityDic[id];
                                    if (CommonUtil.Battle_GetEntityStatus(piece) != ConstUtil.Status_Piece_Pick
                                        && CommonUtil.Battle_GetEntityStatus(piece) != ConstUtil.Status_Piece_Freeze
                                        && CommonUtil.Battle_GetEntityStatus(piece) != ConstUtil.Status_Piece_Out)
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

        private PiecesListComponent UpdateBartenderPiecesList(int player_id, out int bartender_id)
        {
            bartender_id = ConstUtil.None;
            PiecesListComponent piecesListComponent = null;
            Entity player = World.Instance.entityDic[player_id];
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

        public void RefreshBartenderPiecesList(int player_id)
        {
            int bartender_id = ConstUtil.None;
            PiecesListComponent piecesListComponent = UpdateBartenderPiecesList(out bartender_id);
            if (piecesListComponent != null)
            {
                Debug.Log("PiecesPoolSystem Update - prepare start or refresh");
                int random_total = 0;
                if (piecesListComponent.piecesIds.Count <= 0)
                {
                    random_total = piecesListComponent.max_num;
                    piecesListComponent.piecesIds = GetRamdomPiecesFormPool(player_id, random_total);
                }
                else
                {
                    List<int> freezePiecesIds = new List<int>();
                    List<int> unFreezePiecesIds = new List<int>();
                    int freeze_total = 0;
                    for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                    {
                        int piece_id = piecesListComponent.piecesIds[i];
                        Entity piece = World.Instance.entityDic[piece_id];
                        if (CommonUtil.Battle_GetEntityStatus(piece) == ConstUtil.Status_Piece_Freeze && Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_Bartender_Refresh_Pre)
                        {
                            freezePiecesIds.Add(piece_id);
                            freeze_total ++;
                        }
                        else
                        {
                            unFreezePiecesIds.Add(piece_id);
                        }
                    }
                    random_total = piecesListComponent.max_num - freeze_total;
                    CommonUtil.ResetPiecesStatus(unFreezePiecesIds);
                    List<int> randomPiecesIds = GetRamdomPiecesFormPool(player_id, random_total);
                    piecesListComponent.piecesIds = freezePiecesIds.Concat(randomPiecesIds).ToList<int>();
                }
                if (piecesListComponent.piecesIds.Count > 0)
                {
                    for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                    {
                        CommonUtil.SetPieceBelong(piecesListComponent.piecesIds[i], ConstUtil.Belong_Bartender);
                    }
                }
                // TODO: - 1 system 中所有的消息都需要等于 showplayer 才发送
                EventUtil.Instance.SendEvent(ConstUtil.Event_Type_update_bartender_pieces_view, bartender_id);
            }
        }
        public void UpdateBartenderPiecesListFreezeState(int player_id, bool isFreeze = false)
        {
            int bartender_id = ConstUtil.None;
            PiecesListComponent piecesListComponent = UpdateBartenderPiecesList(player_id, out bartender_id);
            if (piecesListComponent != null)
            {
                for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                {
                    int piece_id = piecesListComponent.piecesIds[i];
                    Entity piece = World.Instance.entityDic[piece_id];
                    if (isFreeze)
                    {
                        Debug.Log("PiecesPoolSystem Update - prepare freeze");
                        CommonUtil.Battle_SetEntityStatus(piece, ConstUtil.Status_Piece_Freeze);
                    }
                    else
                    {
                        Debug.Log("PiecesPoolSystem Update - prepare unFreeze");
                        CommonUtil.Battle_SetEntityStatus(piece, ConstUtil.None);
                    }
                }
                EventUtil.Instance.SendEvent(ConstUtil.Event_Type_update_bartender_pieces_view, bartender_id);
            }
        }
        public void PieceBuy(int player_id)
        {
            Debug.Log("BartenderSystem Update - prepare buy");
            Entity player = World.Instance.entityDic[player_id];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    Entity piece = World.Instance.entityDic[playerComponent.piece_buy_id];
                    if (piece != null)
                    {
                        bool isUpdateUI = false;
                        Entity bartender = World.Instance.entityDic[playerComponent.bartender_id];
                        if (bartender != null)
                        {
                            CurrencyComponent bartenderCurrencyComponent = (CurrencyComponent)bartender.GetComponent<CurrencyComponent>();
                            if (bartenderCurrencyComponent != null)
                            {
                                CurrencyComponent pieceCurrencyComponent = (CurrencyComponent)piece.GetComponent<CurrencyComponent>();
                                if (pieceCurrencyComponent != null)
                                {
                                    if (bartenderCurrencyComponent.currency >= pieceCurrencyComponent.piece_cost)
                                    {
                                        bartenderCurrencyComponent.currency -= pieceCurrencyComponent.piece_cost;
                                        EventUtil.Instance.SendEvent(ConstUtil.Event_Type_update_bartender_currency, bartenderCurrencyComponent.currency);

                                        PiecesListComponent piecesListComponent = (PiecesListComponent)bartender.GetComponent<PiecesListComponent>();
                                        if (piecesListComponent != null)
                                        {
                                            piecesListComponent.piecesIds.Remove(piece.ID);
                                            isUpdateUI = true;
                                        }
                                        Entity handCard = World.Instance.entityDic[playerComponent.hand_card_id];
                                        if (handCard != null)
                                        {
                                            PiecesListComponent piecesListComponent = (PiecesListComponent)handCard.GetComponent<PiecesListComponent>();
                                            if (piecesListComponent != null)
                                            {
                                                piecesListComponent.piecesIds.Add(piece.ID);
                                                CommonUtil.SetPieceBelong(piece.ID, ConstUtil.Belong_Hand_Card);
                                                isUpdateUI = true;
                                            }
                                        }
                                    }
                                }
                            }
                            CommonUtil.Battle_SetEntityStatus(piece, ConstUtil.Status_Piece_Out);
                        }
                        if (isUpdateUI)
                        {
                            // TODO: - 1 通知界面更新
                        }
                    }
                    playerComponent.piece_buy_id = ConstUtil.None;
                }
            }
        }
        public void PieceSell(int player_id)
        {
            Debug.Log("BartenderSystem Update - prepare sell");
            Entity player = World.Instance.entityDic[player_id];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    Entity piece = World.Instance.entityDic[playerComponent.piece_sell_id];
                    if (piece != null)
                    {
                        bool isUpdateUI = false;
                        Entity handCard = World.Instance.entityDic[playerComponent.hand_card_id];
                        if (handCard != null)
                        {
                            PiecesListComponent piecesListComponent = (PiecesListComponent)handCard.GetComponent<PiecesListComponent>();
                            if (piecesListComponent != null && piecesListComponent.piecesIds.Contains(piece.ID))
                            {
                                piecesListComponent.piecesIds.Remove(piece.ID);
                                isUpdateUI = true;
                            }
                        }
                        Entity battleCard = World.Instance.entityDic[playerComponent.battle_card_id];
                        if (battleCard != null)
                        {
                            PiecesListComponent piecesListComponent = (PiecesListComponent)battleCard.GetComponent<PiecesListComponent>();
                            if (piecesListComponent != null && piecesListComponent.piecesIds.Contains(piece.ID))
                            {
                                piecesListComponent.piecesIds.Remove(piece.ID);
                                isUpdateUI = true;
                            }
                        }
                        CommonUtil.SetPieceBelong(piece.ID, ConstUtil.Belong_Pool);
                        if (isUpdateUI)
                        {
                            // TODO: - 1 通知界面更新
                        }
                        CurrencyComponent pieceCurrencyComponent = (CurrencyComponent)piece.GetComponent<CurrencyComponent>();
                        if (pieceCurrencyComponent != null)
                        {
                            Entity bartender = World.Instance.entityDic[playerComponent.bartender_id];
                            if (bartender != null)
                            {
                                CurrencyComponent bartenderCurrencyComponent = (CurrencyComponent)bartender.GetComponent<CurrencyComponent>();
                                if (bartenderCurrencyComponent != null)
                                {
                                    bartenderCurrencyComponent.currency += pieceCurrencyComponent.piece_recycle;
                                }
                            }
                            EventUtil.Instance.SendEvent(ConstUtil.Event_Type_update_bartender_currency, bartenderCurrencyComponent.currency);
                        }
                        CommonUtil.Battle_SetEntityStatus(piece, ConstUtil.None);
                    }
                    playerComponent.piece_sell_id = ConstUtil.None;
                }
            }
        }
        public void PieceMove(int player_id)
        {
            Debug.Log("BartenderSystem Update - prepare move");
            Entity player = World.Instance.entityDic[player_id];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    Entity pieceSource = World.Instance.entityDic[playerComponent.piece_move_source_id];
                    if (pieceSource != null)
                    {
                        Entity handCard = World.Instance.entityDic[playerComponent.hand_card_id];
                        PiecesListComponent handCardPiecesListComponent = null;
                        if (handCard != null)
                        {
                            handCardPiecesListComponent = (PiecesListComponent)handCard.GetComponent<PiecesListComponent>();
                        }
                        Entity battleCard = World.Instance.entityDic[playerComponent.battle_card_id];
                        PiecesListComponent battleCardPiecesListComponent = null;
                        if (battleCard != null)
                        {
                            battleCardPiecesListComponent = (PiecesListComponent)battleCard.GetComponent<PiecesListComponent>();
                        }
                        StatusComponent statusComponent = (StatusComponent)pieceSource.GetComponent<StatusComponent>();
                        if (statusComponent != null && handCardPiecesListComponent != null && battleCardPiecesListComponent != null)
                        {
                            switch (statusComponent.status)
                            {
                                case ConstUtil.Status_Piece_Move_B2B:
                                {
                                    Entity pieceTarget = World.Instance.entityDic[playerComponent.piece_move_target_id];
                                    int targetIndex = battleCardPiecesListComponent.piecesIds.IndexOf(pieceTarget.ID);
                                    int sourceIndex = battleCardPiecesListComponent.piecesIds.IndexOf(pieceSource.ID);
                                    battleCardPiecesListComponent.piecesIds[targetIndex] = pieceSource;
                                    battleCardPiecesListComponent.piecesIds[sourceIndex] = pieceTarget;
                                    CommonUtil.SetPieceBelong(piece.ID, ConstUtil.Belong_Battle_Card);
                                    // TODO: - 1 通知界面更新
                                    break;
                                }
                                case ConstUtil.Status_Piece_Move_H2H:
                                {
                                    Entity pieceTarget = World.Instance.entityDic[playerComponent.piece_move_target_id];
                                    int targetIndex = handCardPiecesListComponent.piecesIds.IndexOf(pieceTarget.ID);
                                    int sourceIndex = handCardPiecesListComponent.piecesIds.IndexOf(pieceSource.ID);
                                    handCardPiecesListComponent.piecesIds[targetIndex] = pieceSource;
                                    handCardPiecesListComponent.piecesIds[sourceIndex] = pieceTarget;
                                    CommonUtil.SetPieceBelong(piece.ID, ConstUtil.Belong_Hand_Card);
                                    // TODO: - 1 通知界面更新
                                    break;
                                }
                                case ConstUtil.Status_Piece_Move_B2H:
                                {
                                    battleCardPiecesListComponent.piecesIds.Remove(pieceSource.ID);
                                    handCardPiecesListComponent.piecesIds.Add(pieceSource.ID);
                                    CommonUtil.SetPieceBelong(piece.ID, ConstUtil.Belong_Hand_Card);
                                    // TODO: - 1 通知界面更新
                                    break;
                                }
                                case ConstUtil.Status_Piece_Move_H2B:
                                {
                                    handCardPiecesListComponent.piecesIds.Remove(pieceSource.ID);
                                    battleCardPiecesListComponent.piecesIds.Add(pieceSource.ID);
                                    CommonUtil.SetPieceBelong(piece.ID, ConstUtil.Belong_Battle_Card);
                                    // TODO: - 1 通知界面更新
                                    break;
                                }
                                default:
                                {
                                    break;
                                }
                            }
                        }
                    }
                    playerComponent.piece_move_source_id = ConstUtil.None;
                    playerComponent.piece_move_target_id = ConstUtil.None;
                }
            }
        }
        public override void Update()
        {
            List<int> player_list = Process.GetInstance().GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                if (Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Game_Start_Heroes_Pool)
                {
                    Debug.Log("PiecesPoolSystem Update - init");
                    GeneratePoolFormConfig();
                    Process.GetInstance().SetProcess(ConstUtil.Process_Game_Start_Pieces_Pool, player_id);
                }
                else if (Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_Bartender_Refresh_Pre || Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_Bartender_Refresh)
                {
                    // 从池子中抽取当前玩家酒馆的棋子信息
                    RefreshBartenderPiecesList(player_id);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Ing, player_id);
                }
                else if (Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_Bartender_Freeze)
                {
                    // 冻结酒馆的棋子
                    UpdateBartenderPiecesListFreezeState(player_id, true);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Ing, player_id);
                }
                else if (Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_Bartender_UnFreeze)
                {
                    // 解除冻结酒馆的棋子
                    UpdateBartenderPiecesListFreezeState(player_id);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Ing, player_id);
                }
                else if (Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_Piece_Buy)
                {
                    PieceBuy(player_id);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Ing, player_id);
                }
                else if (Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_Piece_Sell)
                {
                    PieceSell(player_id);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Ing, player_id);
                }
                else if (Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_Piece_Move)
                {
                    PieceMove(player_id);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Ing, player_id);
                }
                else if (Process.GetInstance().GetProcess(player_id) == ConstUtil.Process_Prepare_End)
                {
                    // TODO: 回合结束的冻结的棋子
                }
            }
        }
    }
}
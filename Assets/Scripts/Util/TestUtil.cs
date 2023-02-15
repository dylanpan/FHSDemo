using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Component;

namespace Chess.Util
{
    // DataUtil 通过该类对已有的数据进行更新
    public class TestUtil
    {
        public static void SetHero(int id, int player_id = ConstUtil.None)
        {
            if (player_id == ConstUtil.None)
            {
                player_id = Process.GetInstance().GetShowPlayerId();
            }
            Entity entity = World.Instance.entityDic[player_id];
            PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
            if (playerComponent != null)
            {
                if (playerComponent.hero_id == ConstUtil.None)
                {
                    playerComponent.hero_id = id;
                }
            }
        }

        public static void SetBartender(int config_id, int player_id = ConstUtil.None)
        {
            if (player_id == ConstUtil.None)
            {
                player_id = Process.GetInstance().GetShowPlayerId();
            }
            Entity player = World.Instance.entityDic[player_id];
            PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
            if (playerComponent != null)
            {
                if (playerComponent.bartender_id == ConstUtil.None)
                {
                    Entity? bartenderEntity = null;
                    List<int> bartender_pool = Process.GetInstance().GetBartenderPool();
                    for (int i = 0; i < bartender_pool.Count; i++)
                    {
                        Entity entity = World.Instance.entityDic[bartender_pool[i]];
                        NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
                        StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                        if (nameComponent != null && nameComponent.id == config_id)
                        {
                            bartenderEntity = entity;
                            break;
                        }
                    }
                    playerComponent.bartender_id = bartenderEntity.ID;
                }
            }
        }

        public static void SetSellPieceId(int id)
        {
            Entity player = World.Instance.entityDic[Process.GetInstance().GetShowPlayerId()];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    playerComponent.piece_sell_id = id;
                }
            }
        }
        public static void SetMovePieceId(int source_id = ConstUtil.None, int target_id = ConstUtil.None)
        {
            Entity player = World.Instance.entityDic[Process.GetInstance().GetShowPlayerId()];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    playerComponent.piece_move_source_id = source_id;
                    playerComponent.piece_move_target_id = target_id;
                }
            }
        }
        public static void SetBuyPieceId(int id)
        {
            Entity player = World.Instance.entityDic[Process.GetInstance().GetShowPlayerId()];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    playerComponent.piece_buy_id = id;
                }
            }
        }
        
        public static int GetTestPieces(int piece_id)
        {
            int find_id = 0;
            foreach (Entity entity in World.Instance.entityDic.Values)
            {
                if (CommonUtil.CheckIsPiece(entity))
                {
                    NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
                    if (nameComponent != null && nameComponent.id == piece_id)
                    {
                        find_id = entity.ID;
                    }
                }
            }
            return find_id;
        }
        public static void SetTestPiecesIds(ref Entity aEntity, ref Entity bEntity)
        {
            if (aEntity != null)
            {
                PiecesListComponent aPiecesListComponent = (PiecesListComponent)aEntity.GetComponent<PiecesListComponent>();
                if (aPiecesListComponent != null)
                {
                    int pieces_0_ID = TestUtil.GetTestPieces(4001);
                    int pieces_1_ID = TestUtil.GetTestPieces(4002);
                    int pieces_2_ID = TestUtil.GetTestPieces(4003);
                    int[] piecesIds = {pieces_0_ID, pieces_1_ID, pieces_2_ID};
                    aPiecesListComponent.piecesIds = new List<int>(piecesIds);
                }
            }
            if (bEntity != null)
            {
                PiecesListComponent bPiecesListComponent = (PiecesListComponent)bEntity.GetComponent<PiecesListComponent>();
                if (bPiecesListComponent != null)
                {
                    int pieces_0_ID = TestUtil.GetTestPieces(4004);
                    int pieces_1_ID = TestUtil.GetTestPieces(4005);
                    int pieces_2_ID = TestUtil.GetTestPieces(4005);
                    int[] piecesIds = {pieces_0_ID, pieces_1_ID, pieces_2_ID};
                    bPiecesListComponent.piecesIds = new List<int>(piecesIds);
                }
            }
        }
    }
}
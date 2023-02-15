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

        public static void SetSellPieceId(int id, int player_id = ConstUtil.None)
        {
            if (player_id == ConstUtil.None)
            {
                player_id = Process.GetInstance().GetShowPlayerId();
            }
            Entity player = World.Instance.entityDic[player_id];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    playerComponent.piece_sell_id = id;
                }
            }
        }
        public static void SetMovePieceId(int source_id = ConstUtil.None, int target_id = ConstUtil.None, int player_id = ConstUtil.None)
        {
            if (player_id == ConstUtil.None)
            {
                player_id = Process.GetInstance().GetShowPlayerId();
            }
            Entity player = World.Instance.entityDic[player_id];
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
        public static void SetBuyPieceId(int id, int player_id = ConstUtil.None)
        {
            if (player_id == ConstUtil.None)
            {
                player_id = Process.GetInstance().GetShowPlayerId();
            }
            Entity player = World.Instance.entityDic[player_id];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    playerComponent.piece_buy_id = id;
                }
            }
        }
    }
}
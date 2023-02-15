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
    public class MatchSystem : ISystem
    {
        public void matchPlayer(int player_id)
        {
            Entity source_player = World.Instance.entityDic[player_id];
            PlayerComponent source_playerComponent = (PlayerComponent)source_player.GetComponent<PlayerComponent>();
            if (source_playerComponent != null && source_playerComponent.rival_id == ConstUtil.None)
            {
                // 随机一个非本身且没有匹配到对手的玩家
                List<int> player_id_list = Process.GetInstance().GetPlayerIdList();
                while (true)
                {
                    int rival_id = player_id_list[CommonUtil.RandomPlayerIndex(player_id_list.Count)];
                    if (rival_id != player_id)
                    {
                        Entity rival_player = World.Instance.entityDic[rival_id];
                        PlayerComponent rival_playerComponent = (PlayerComponent)rival_player.GetComponent<PlayerComponent>();
                        if (rival_playerComponent != null && rival_playerComponent.rival_id == ConstUtil.None)
                        {
                            source_playerComponent.rival_id = rival_player.ID;
                            rival_playerComponent.rival_id = source_player.ID;
                        }
                        break;
                    }
                }
            }
        }
        public override void Update()
        {
            List<int> player_list = Process.GetInstance().GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Match_Start))
                {
                    Debug.Log("MatchSystem Update - start match");
                    matchPlayer(player_id);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Match_End, player_id);
                }
                else if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Match_End))
                {
                    Process.GetInstance().SetProcess(ConstUtil.Process_Battle_Start, player_id);
                }
            }
        }
    }
}
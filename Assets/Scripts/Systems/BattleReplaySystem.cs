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
    public class BattleReplaySystem : ISystem
    {
        public override void Update()
        {
            List<int> player_list = Process.GetInstance().GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Battle_End))
                {
                    Debug.Log("BattleReplaySystem Update - init");
                    Process.GetInstance().SetProcess(ConstUtil.Process_Battle_Replay_Start, player_id);
                }
                else if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Battle_Replay_Start))
                {
                    Process.GetInstance().SetProcess(ConstUtil.Process_Battle_Replay_End, player_id);
                }
                else if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Battle_Replay_End))
                {
                    // TODO: 判断游戏是否结束
                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Start, player_id);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Game_End, player_id);
                }
            }
        }
    }
}
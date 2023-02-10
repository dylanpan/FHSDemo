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
                if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Game_End))
                {
                    Debug.Log("BattleReplaySystem Update - init");
                    // Process.GetInstance().SetProcess(ConstUtil.Process_Game_End);
                }
            }
        }
    }
}
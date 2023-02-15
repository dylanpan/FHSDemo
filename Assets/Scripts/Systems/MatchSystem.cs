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
        public override void Update()
        {
            List<int> player_list = Process.GetInstance().GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Match_Start))
                {
                    // TODO: - 11 输出匹配结果
                    Debug.Log("MatchSystem Update - start match");
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
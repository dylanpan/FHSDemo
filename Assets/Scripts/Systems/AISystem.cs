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
    public class AISystem : ISystem
    {
        public override void Update()
        {
            List<int> player_list = Process.Instance.GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                // TODO: - 1 通过获取 AI 的行为配置表进行对应的操作执行
                if (Process.Instance.GetProcess(player_id) == ConstUtil.Process_Game_End)
                {
                    Debug.Log("AISystem Update - init");
                }
            }
        }
    }
}
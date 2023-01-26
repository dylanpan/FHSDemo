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
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_End)
            {
                Debug.Log("BattleReplaySystem Update - init");
                // Process.Instance.SetProcess(ConstUtil.Process_Game_End);
            }
        }
    }
}
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
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_End)
            {
                Debug.Log("AISystem Update - init");
            }
        }
    }
}
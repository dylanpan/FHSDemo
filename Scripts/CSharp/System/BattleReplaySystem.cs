using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Config;
using Chess.Component;

namespace Chess.System
{
    public class BattleReplaySystem : ISystem
    {
        public override void Update()
        {
            Console.WriteLine("BattleReplaySystem Update");
            
            List<Entity> battleEntitys = new List<Entity>();
            foreach (Entity entity in World.Instance.entityDic.Values)
            {
            }
        }
    }
}
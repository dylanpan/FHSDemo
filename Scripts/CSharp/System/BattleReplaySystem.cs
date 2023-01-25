using System;
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
            for (int i = 0; i < World.Instance.entityDic.Values.Count; i++)
            {
                Entity entity = World.Instance.entityDic.Values.ElementAt(i);
            }
        }
    }
}
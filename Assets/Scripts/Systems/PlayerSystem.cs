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
    public class PlayerSystem : ISystem
    {
        public Entity CreatePlayerEntity()
        {
            Entity player = new Entity();
            player.AddComponent(new NameComponent(){name = "Player_" + player.ID});
            player.AddComponent(new PlayerComponent());
            return player;
        }
        public void InitWorldPlayerEntity()
        {
            // TODO: 新增扩展到 8 个
            for (int i = 0; i < ConstUtil.Max_Num_Player; i++)
            {
                World.Instance.AddEntity(CreatePlayerEntity());
            }
        }

        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_Start)
            {
                Debug.Log("PlayerSystem Update - init");
                InitWorldPlayerEntity();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Player);
            }
        }
    }
}
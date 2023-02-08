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
        public Entity CreatePlayerEntity(int player_type)
        {
            Entity entity = new Entity();
            entity.AddComponent(new NameComponent(){name = "Player_" + entity.ID});
            entity.AddComponent(new PlayerComponent() {ai_id = (player_type == ConstUtil.Player_Type_AI) ? entity.ID : ConstUtil.None});
            entity.AddComponent(new StatusComponent());
            World.Instance.AddEntity(entity);
            return entity;
        }
        public void InitWorldPlayerEntity()
        {
            List<int> player_list = new List<int>();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_type = player_list[i];
                Entity entity = CreatePlayerEntity(player_type);
                if (player_type == ConstUtil.Player_Type_Human_Mine)
                {
                    Process.Instance.SetShowPlayerId(entity.ID);
                }
            }
        }

        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Main_View)
            {
                Debug.Log("PlayerSystem Update - init");
                InitWorldPlayerEntity();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Player);
            }
        }
    }
}
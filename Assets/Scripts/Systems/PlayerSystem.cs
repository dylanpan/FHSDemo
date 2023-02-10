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
        public Entity CreatePlayerEntity(int player_type, AIConfig aiConfig)
        {
            Entity entity = new Entity();
            entity.AddComponent(new NameComponent(){name = "Player_" + entity.ID});
            entity.AddComponent(new PlayerComponent() {ai_id = (player_type == ConstUtil.Player_Type_AI) ? entity.ID : ConstUtil.None});
            entity.AddComponent(new StatusComponent());
            // TODO: - 1 建立 AI 的池子以及配置表
            entity.AddComponent(new ConfigComponent<AIConfig>(){config = aiConfig});
            World.Instance.AddEntity(entity);
            return entity;
        }
        public void InitWorldPlayerEntity()
        {
            List<int> player_type_list = Process.GetInstance().GetPlayerTypeList();
            for (int i = 0; i < player_type_list.Count; i++)
            {
                int player_type = player_type_list[i];
                Entity entity = CreatePlayerEntity(player_type);
                Process.GetInstance().SetPlayerIdList(entity.ID);
                if (player_type == ConstUtil.Player_Type_Human_Mine)
                {
                    Process.GetInstance().SetShowPlayerId(entity.ID);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Game_Start_Player, entity.ID);
                }
            }
        }

        public override void Update()
        {
            // TODO: - 1 将所有 update 的地方进行修改，切换成 NotificationQueue ，然后通过增加当前状态下的 action 进行处理？？？目的是给每一个玩家一个队列，增加什么执行什么，不进行整体遍历
            if (Process.GetInstance().CheckProcessIsEqual(Process.GetInstance().GetShowPlayerId(), ConstUtil.Process_Game_Start_Main_View))
            {
                Debug.Log("PlayerSystem Update - init");
                InitWorldPlayerEntity();
            }
        }
    }
}
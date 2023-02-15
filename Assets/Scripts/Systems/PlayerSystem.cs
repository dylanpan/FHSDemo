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
            
            Entity entity = null;
            if (player_type == ConstUtil.Player_Type_Human_Mine || player_type == ConstUtil.Player_Type_Human_Other)
            {
                entity = new Entity();
                entity.AddComponent(new NameComponent(){name = "Player_" + entity.ID});
                entity.AddComponent(new PlayerComponent());
                entity.AddComponent(new StatusComponent());
                entity.AddComponent(new ConfigComponent<AIConfig>());
                World.Instance.AddEntity(entity);
            }
            else
            {
                List<int> ai_pool = Process.GetInstance().GetAIPool();
                foreach (int ai_id in ai_pool)
                {
                    Entity aiEntity = World.Instance.entityDic[ai_id];
                    if (aiEntity != null)
                    {
                        ConfigComponent<AIConfig> configComponent = (ConfigComponent<AIConfig>)aiEntity.GetComponent<ConfigComponent<AIConfig>>();
                        if (configComponent != null)
                        {
                            if (configComponent.config.type == player_type)
                            {
                                entity = CommonUtil.CopyEntity(aiEntity);
                                NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
                                if (nameComponent != null)
                                {
                                    nameComponent.name = "Player_" + nameComponent.name;
                                }
                                PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
                                if (PlayerComponent != null)
                                {
                                    playerComponent.ai_id = entity.ID;
                                }
                            }
                        }
                    }
                }
            }
            return entity;
        }
        public void InitWorldPlayerEntity()
        {
            List<int> player_type_list = Process.GetInstance().GetPlayerTypeList();
            for (int i = 0; i < player_type_list.Count; i++)
            {
                int player_type = player_type_list[i];
                if (player_type != ConstUtil.None)
                {
                    Entity entity = CreatePlayerEntity(player_type);
                    Process.GetInstance().SetPlayerIdList(entity.ID);
                    if (player_type == ConstUtil.Player_Type_Human_Mine)
                    {
                        Process.GetInstance().SetShowPlayerId(entity.ID);
                        Process.GetInstance().SetProcess(ConstUtil.Process_Game_Start_Player, entity.ID);
                    }
                }
            }
        }

        public override void Update()
        {
            // TODO: - 1 优化点：目的是给每一个玩家一个队列，增加什么执行什么，不进行整体遍历
            if (Process.GetInstance().CheckProcessIsEqual(Process.GetInstance().GetShowPlayerId(), ConstUtil.Process_Game_Start_Main_View))
            {
                Debug.Log("PlayerSystem Update - init");
                InitWorldPlayerEntity();
            }
        }
    }
}
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
        public void CreateAIEntity(int player_type, AIConfig aiConfig)
        {
            Entity entity = new Entity();
            entity.AddComponent(new NameComponent(){name = "AI_" + aiConfig.name + "_" + entity.ID});
            entity.AddComponent(new PlayerComponent());
            entity.AddComponent(new StatusComponent());
            entity.AddComponent(new ConfigComponent<AIConfig>(){config = aiConfig});
            World.Instance.AddEntity(entity);
            Process.GetInstance().AddAIToPool(entity.ID);
        }
        public void GeneratePoolFormConfig()
        {
            List<AIConfig> configDataList = ConfigUtil.GetConfigDataList<AIConfig>(ConstUtil.Json_File_AI_Config);
            if (configDataList.Count > 0)
            {
                for (int i = 0; i < configDataList.Count; i++)
                {
                    AIConfig aiConfig = configDataList[i];
                    CreateAIEntity(aiConfig);
                }
            }
            else
            {
                Debug.Log("AISystem get empty config");
            }
        }
        public override void Update()
        {
            List<int> player_list = Process.GetInstance().GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                if (Process.GetInstance().CheckProcessIsEqual(Process.GetInstance().GetShowPlayerId(), ConstUtil.Process_Game_Start))
                {
                    Debug.Log("AISystem Update - init");
                    GeneratePoolFormConfig();
                    Process.GetInstance().SetProcess(ConstUtil.Process_Game_Start_AI_Pool, Process.GetInstance().GetShowPlayerId());
                }
                else if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Game_End))
                {
                    // TODO: - 11 ???????????? AI ?????????????????????????????????????????????
                    /**
                    1. AI ???????????????
                    2. ??????????????????????????? ai ???????????????
                    3. ????????????????????????????????????
                        ?????????
                        ?????????
                        ??????????????????????????????????????????
                    **/
                }
            }
        }
    }
}
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
    public class BartenderSystem : ISystem
    {
        public void GrenerateBartenderEntity(BartenderConfig bartenderConfig)
        {
            if (bartenderConfig != null)
            {
                Entity entity = new Entity();
                entity.AddComponent(new NameComponent(){name = bartenderConfig.name, id = bartenderConfig.id});
                entity.AddComponent(new SkinComponent(){skin_name = bartenderConfig.skin_name});
                entity.AddComponent(new LevelComponent(){level = Process.Instance.GetCurrentLevel()});
                entity.AddComponent(new CurrencyComponent(){currency = Process.Instance.GetCurrentCurrency(), up_level_cost = bartenderConfig.up_level_cost[Process.Instance.GetCurrentLevel()-1], refresh_cost = bartenderConfig.refresh_cost});
                entity.AddComponent(new PiecesListComponent(){max_num = bartenderConfig.level_list_num[Process.Instance.GetCurrentLevel()-1], bartender_id = entity.ID});
                entity.AddComponent(new StatusComponent());
                World.Instance.AddEntity(entity);
                Process.Instance.AddBartenderToPool(entity.ID);
            }
        }
        public void GeneratePoolFormConfig()
        {
            List<BartenderConfig> configDataList = ConfigUtil.GetConfigData<BartenderConfig>(ConstUtil.Json_File_Bartender_Config);
            if (configDataList.Count > 0)
            {
                for (int i = 0; i < configDataList.Count; i++)
                {
                    BartenderConfig bartenderConfig = configDataList[i];
                    GrenerateBartenderEntity(bartenderConfig);
                }
            }
            else
            {
                Debug.Log("BartenderSystem get empty config");
            }
        }
        public void UpdateBartenderInfo()
        {

        }
        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Player)
            {
                Debug.Log("BartenderSystem Update - init");
                GeneratePoolFormConfig();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Bartender);
            }
            else if (Process.Instance.GetProcess() == ConstUtil.Process_Prepare_Start)
            {
                // Debug.Log("BartenderSystem Update - battle prepare");
                // TODO: 设置当前回合的当前玩家的酒馆信息
                UpdateBartenderInfo();
            }
        }
    }
}
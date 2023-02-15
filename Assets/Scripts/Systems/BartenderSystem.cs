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
                entity.AddComponent(new LevelComponent(){level = ConstUtil.Init_Level});
                entity.AddComponent(new CurrencyComponent(){currency = ConstUtil.Init_Currency, up_level_cost = bartenderConfig.up_level_cost[ConstUtil.Init_Level-1], refresh_cost = bartenderConfig.refresh_cost});
                entity.AddComponent(new PiecesListComponent(){max_num = bartenderConfig.level_list_num[ConstUtil.Init_Level-1], bartender_id = entity.ID});
                entity.AddComponent(new StatusComponent());
                entity.AddComponent(new ConfigComponent<BartenderConfig>(){config = bartenderConfig});
                World.Instance.AddEntity(entity);
                Process.GetInstance().AddBartenderToPool(entity.ID);
            }
        }
        public void GeneratePoolFormConfig()
        {
            List<BartenderConfig> configDataList = ConfigUtil.GetConfigDataList<BartenderConfig>(ConstUtil.Json_File_Bartender_Config);
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
        public void BartenderLevelUp(int player_id)
        {
            Entity player = World.Instance.entityDic[player_id];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    Entity bartender = World.Instance.entityDic[playerComponent.bartender_id];
                    if (bartender != null)
                    {
                        LevelComponent levelComponent = (LevelComponent)bartender.GetComponent<LevelComponent>();
                        CurrencyComponent currencyComponent = (CurrencyComponent)bartender.GetComponent<CurrencyComponent>();
                        PiecesListComponent piecesListComponent = (PiecesListComponent)bartender.GetComponent<PiecesListComponent>();
                        ConfigComponent<BartenderConfig> configComponent = (ConfigComponent<BartenderConfig>)bartender.GetComponent<ConfigComponent<BartenderConfig>>();
                        if (levelComponent != null && currencyComponent != null && piecesListComponent != null && configComponent != null)
                        {
                            if (levelComponent.level < ConstUtil.Max_Level && currencyComponent.currency >= currencyComponent.up_level_cost)
                            {
                                levelComponent.level ++;
                                currencyComponent.currency -= currencyComponent.up_level_cost;
                                currencyComponent.up_level_cost = configComponent.config.up_level_cost[levelComponent.level-1];
                                piecesListComponent.max_num = configComponent.config.level_list_num[levelComponent.level-1];
                                EventUtil.Instance.SendEvent(ConstUtil.Event_Type_update_bartender_currency, currencyComponent.currency);
                                EventUtil.Instance.SendEvent(ConstUtil.Event_Type_update_bartender_level, levelComponent.level);
                            }
                        }
                    }
                }
            }
        }
        public void BartenderRefresh(int player_id, bool isCost)
        {
            Entity player = World.Instance.entityDic[player_id];
            if (player != null)
            {
                PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    Entity bartender = World.Instance.entityDic[playerComponent.bartender_id];
                    if (bartender != null)
                    {
                        LevelComponent levelComponent = (LevelComponent)bartender.GetComponent<LevelComponent>();
                        CurrencyComponent currencyComponent = (CurrencyComponent)bartender.GetComponent<CurrencyComponent>();
                        if (levelComponent != null && currencyComponent != null)
                        {
                            if (isCost)
                            {
                                if (currencyComponent.currency >= currencyComponent.refresh_cost)
                                {
                                    currencyComponent.currency -= currencyComponent.refresh_cost;
                                    EventUtil.Instance.SendEvent(ConstUtil.Event_Type_update_bartender_currency, currencyComponent.currency);
                                }
                                else
                                {
                                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Ing, player_id);
                                }
                            }
                        }
                    }
                }
            }
        }
        public void UpdateBartenderInfo(int player_id)
        {

        }
        public override void Update()
        {
            List<int> player_list = Process.GetInstance().GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                if (Process.GetInstance().CheckProcessIsEqual(Process.GetInstance().GetShowPlayerId(), ConstUtil.Process_Game_Start_AI_Pool))
                {
                    Debug.Log("BartenderSystem Update - init");
                    GeneratePoolFormConfig();
                    Process.GetInstance().SetProcess(ConstUtil.Process_Game_Start_Bartender_Pool, Process.GetInstance().GetShowPlayerId());
                }
                else if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Prepare_Bartender_Level_Up))
                {
                    Debug.Log("BartenderSystem Update - prepare level up");
                    BartenderLevelUp(player_id);
                    Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Ing, player_id);
                }
                else if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Prepare_Bartender_Refresh_Pre))
                {
                    Debug.Log("BartenderSystem Update - prepare refresh pre");
                    BartenderRefresh(player_id, false);
                }
                else if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Prepare_Bartender_Refresh))
                {
                    Debug.Log("BartenderSystem Update - prepare refresh");
                    BartenderRefresh(player_id, true);
                }
                else if (Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Prepare_End))
                {
                    // Debug.Log("BartenderSystem Update - battle prepare end");
                    // TODO: 设置当前回合的当前玩家的酒馆信息，如：当回合玩家拥有金额
                    UpdateBartenderInfo(player_id);
                }
            }
        }
    }
}
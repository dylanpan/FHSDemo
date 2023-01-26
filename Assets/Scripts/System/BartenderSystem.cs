using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Config;
using Chess.Component;
using Chess.Util;

namespace Chess.System
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
                entity.AddComponent(new LevelComponent(){current_level = Process.Instance.current_level});
                entity.AddComponent(new CurrencyComponent(){currency = Process.Instance.current_currency, up_level_cost = bartenderConfig.up_level_cost[Process.Instance.current_level-1], refresh_cost = bartenderConfig.refresh_cost});
                entity.AddComponent(new PiecesListComponent(){max_num = bartenderConfig.level_list_num[Process.Instance.current_level-1], bartender_id = entity.ID});
                entity.AddComponent(new StatusComponent());
                World.Instance.AddEntity(entity);
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
                Console.WriteLine("BartenderSystem get empty config");
            }
        }
        public override void Update()
        {
            Console.WriteLine("BartenderSystem Update");
            GeneratePoolFormConfig();
            TestUtil.SetBartender();
        }
    }
}
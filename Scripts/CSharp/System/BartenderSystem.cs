using System;

public class BartenderSystem : ISystem
{
    public void GrenerateBartenderEntity(Dictionary<string, string> paramDict)
    {
        Entity entity = new Entity();
        int _id = ConstUtil.Zero;
        int.TryParse(paramDict["id"], out _id);
        entity.AddComponent(new NameComponent(){name = paramDict["name"], id = _id});
        entity.AddComponent(new SkinComponent(){skin_name = paramDict["skin_name"]});
        // TODO: 当前等级、以及对应的当前回合金币和棋子队列上限的存储
        entity.AddComponent(new LevelComponent(){current_level = 1});
        int _refresh_cost = ConstUtil.Zero;
        int.TryParse(paramDict["refresh_cost"], out _refresh_cost);
        entity.AddComponent(new CurrencyComponent(){currency = 3, up_level_cost = 4, refresh_cost = _refresh_cost});
        entity.AddComponent(new PiecesListComponent(){max_num = 3, bartender_id = entity.ID});
        entity.AddComponent(new StatusComponent());
        World.Instance.AddEntity(entity);
    }
    public void GeneratePoolFormConfig()
    {
        List<Dictionary<string, string>> bartenderConfigList = ConfigUtil.GetBartenderConfig();
        for (int i = 0; i < bartenderConfigList.Count; i++)
        {
            Dictionary<string, string> configDict = bartenderConfigList[i];
            Dictionary<string, string> paramDict = Util.GetCommonParamDict();
            foreach (KeyValuePair<string, string> config in configDict)
            {
                paramDict[config.Key] = config.Value;
            }
            GrenerateBartenderEntity(paramDict);
        }
    }
    public override void Update()
    {
        Console.WriteLine("BartenderSystem Update");
        GeneratePoolFormConfig();
        TestUtil.SetBartender();
    }
}
using System;

public class HeroPoolSystem : ISystem
{
    public void GenerateHeroEntity(Dictionary<string, string> paramDict)
    {
        Entity hero = new Entity();
        int _id = ConstUtil.Zero;
        int.TryParse(paramDict["id"], out _id);
        hero.AddComponent(new NameComponent(){name = paramDict["name"], id = _id});
        hero.AddComponent(new SkinComponent(){skin_name = paramDict["skin_name"]});
        int _skill_id = ConstUtil.Zero;
        int.TryParse(paramDict["skill_id"], out _skill_id);
        hero.AddComponent(new SkillComponent(){skill_id = _skill_id});
        int _atk = ConstUtil.Zero;
        int.TryParse(paramDict["atk"], out _atk);
        int _hp = ConstUtil.Zero;
        int.TryParse(paramDict["hp"], out _hp);
        hero.AddComponent(new PorpertyComponent(){atk = _atk, hp = _hp});
        hero.AddComponent(new StatusComponent());
        World.Instance.AddEntity(hero);
    }
    public void GeneratePoolFormConfig()
    {
        List<Dictionary<string, string>> heroesConfigList = ConfigUtil.GetHeroesConfig();
        for (int i = 0; i < heroesConfigList.Count; i++)
        {
            Dictionary<string, string> configDict = heroesConfigList[i];
            Dictionary<string, string> paramDict = Util.GetCommonParamDict();
            foreach (KeyValuePair<string, string> config in configDict)
            {
                paramDict[config.Key] = config.Value;
            }
            GenerateHeroEntity(paramDict);
        }
    }
    public override void Update()
    {
        Console.WriteLine("HeroPoolSystem Update");
        GeneratePoolFormConfig();
        TestUtil.CheckAddHeroEntity();
    }
}
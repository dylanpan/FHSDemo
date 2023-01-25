using System;

public class HeroPoolSystem : ISystem
{
    public void GenerateHeroEntity(HeroesConfig heroesConfig)
    {
        Entity entity = new Entity();
        entity.AddComponent(new NameComponent(){name = heroesConfig.name, id = heroesConfig.id});
        entity.AddComponent(new SkinComponent(){skin_name = heroesConfig.skin_name});
        entity.AddComponent(new SkillComponent(){skill_id = heroesConfig.skill_id});
        entity.AddComponent(new PorpertyComponent(){atk = heroesConfig.atk, hp = heroesConfig.hp});
        entity.AddComponent(new StatusComponent());
        World.Instance.AddEntity(entity);
    }
    public void GeneratePoolFormConfig()
    {
        List<HeroesConfig> configDataList = ConfigUtil.GetConfigData<HeroesConfig>(ConstUtil.JsonFile_HeroesConfig);
        for (int i = 0; i < configDataList.Count; i++)
        {
            HeroesConfig heroesConfig = configDataList[i];
            GenerateHeroEntity(heroesConfig);
        }
    }
    public override void Update()
    {
        Console.WriteLine("HeroPoolSystem Update");
        GeneratePoolFormConfig();
        TestUtil.SetHero();
    }
}
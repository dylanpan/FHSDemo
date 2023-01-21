using System;

public class HeroPoolSystem : ISystem
{
    public Entity CreateHeroEntity()
    {
        Entity hero = new Entity();
        return hero;
    }
    public void AddHeroComponents(Entity entity)
    {
        if (entity.GetComponent<NameComponent>() == null)
        {
            entity.AddComponent(new NameComponent(){name = "迦拉克隆"});
        }
        if (entity.GetComponent<SkinComponent>() == null)
        {
            entity.AddComponent(new SkinComponent(){skin_name = "jk_001"});
        }
        if (entity.GetComponent<SkillComponent>() == null)
        {
            entity.AddComponent(new SkillComponent(){skill_id = 1001});
        }
        if (entity.GetComponent<PorpertyComponent>() == null)
        {
            entity.AddComponent(new PorpertyComponent(){atk = 0, hp = 40});
        }
    }
    public void CheckAddHeroEntity(ref Entity entity)
    {
        PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
        if (playerComponent != null)
        {
            if (playerComponent.hero_id == -1)
            {
                Entity hero = CreateHeroEntity();
                playerComponent.hero_id = hero.ID;
                AddHeroComponents(hero);
                World.Instance.AddEntity(hero);
            }
        }
    }
    public override void Update()
    {
        Console.WriteLine("HeroPoolSystem Update");
        for (int i = 0; i < World.Instance.entityDic.Values.Count; i++)
        {
            Entity entity = World.Instance.entityDic.Values.ElementAt(i);
            CheckAddHeroEntity(ref entity);
        }
    }
}
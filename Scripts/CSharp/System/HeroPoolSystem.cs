using System;

public class HeroPoolSystem : ISystem
{
    public Entity CreateHeroEntity()
    {
        Entity hero = new Entity();
        return hero;
    }
    public void CheckAddHeroEntity(ref Entity entity)
    {
        PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
        if (playerComponent != null)
        {
            if (playerComponent.hero_id == ConstUtil.None)
            {
                Entity hero = CreateHeroEntity();
                playerComponent.hero_id = hero.ID;
                TestUtil.AddHeroComponents(hero);
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
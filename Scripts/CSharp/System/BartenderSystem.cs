using System;

public class BartenderSystem : ISystem
{
    public Entity CreateBartenderEntity()
    {
        Entity bartender = new Entity();
        return bartender;
    }
    public void AddBartenderComponents(Entity entity)
    {
        if (entity.GetComponent<NameComponent>() == null)
        {
            entity.AddComponent(new NameComponent(){name = "调酒机器人"});
        }
        if (entity.GetComponent<CurrencyComponent>() == null)
        {
            entity.AddComponent(new CurrencyComponent(){currency = 3, up_level_cost = 4, refresh_cost = 1});
        }
        if (entity.GetComponent<LevelComponent>() == null)
        {
            entity.AddComponent(new LevelComponent(){current_level = 1});
        }
        if (entity.GetComponent<SkinComponent>() == null)
        {
            entity.AddComponent(new SkinComponent(){skin_name = "wine_001"});
        }
        if (entity.GetComponent<PiecesListComponent>() == null)
        {
            // int[] bartender_piecesIds = {10, 11, 12};
            // piecesIds = new List<int>(bartender_piecesIds)
            entity.AddComponent(new PiecesListComponent(){max_num = 3, bartender_id = entity.ID});
        }
    }
    public void CheckAddBartenderEntity(ref Entity entity)
    {
        PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
        if (playerComponent != null)
        {
            if (playerComponent.bartender_id == -1)
            {
                Entity bartender = CreateBartenderEntity();
                playerComponent.bartender_id = bartender.ID;
                AddBartenderComponents(bartender);
                World.Instance.AddEntity(bartender);
            }
        }
    }
    public override void Update()
    {
        Console.WriteLine("BartenderSystem Update");
        for (int i = 0; i < World.Instance.entityDic.Values.Count; i++)
        {
            Entity entity = World.Instance.entityDic.Values.ElementAt(i);
            CheckAddBartenderEntity(ref entity);
        }
    }
}
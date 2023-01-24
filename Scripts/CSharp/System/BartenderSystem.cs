using System;

public class BartenderSystem : ISystem
{
    public Entity CreateBartenderEntity()
    {
        Entity bartender = new Entity();
        return bartender;
    }
    public void CheckAddBartenderEntity(ref Entity entity)
    {
        PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
        if (playerComponent != null)
        {
            if (playerComponent.bartender_id == ConstUtil.None)
            {
                Entity bartender = CreateBartenderEntity();
                playerComponent.bartender_id = bartender.ID;
                TestUtil.AddBartenderComponents(bartender);
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
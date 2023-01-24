using System;

public class HandCardSystem : ISystem
{
    public Entity CreateHandCardEntity()
    {
        Entity entity = new Entity();
        entity.AddComponent(new NameComponent(){name = "HandCard"});
        entity.AddComponent(new PiecesListComponent(){max_num = ConstUtil.Max_HandCardNum, hand_card_id = entity.ID});
        return entity;
    }
    public void CheckAddHandCardEntity(ref Entity entity)
    {
        PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
        if (playerComponent != null)
        {
            if (playerComponent.hand_card_id == ConstUtil.None)
            {
                Entity handCard = CreateHandCardEntity();
                playerComponent.hand_card_id = handCard.ID;
                World.Instance.AddEntity(handCard);
            }
        }
    }
    public override void Update()
    {
        Console.WriteLine("HandCardSystem Update");
        for (int i = 0; i < World.Instance.entityDic.Values.Count; i++)
        {
            Entity entity = World.Instance.entityDic.Values.ElementAt(i);
            CheckAddHandCardEntity(ref entity);
        }
    }
}
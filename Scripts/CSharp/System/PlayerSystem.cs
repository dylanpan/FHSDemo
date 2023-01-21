using System;

public class PlayerSystem : ISystem
{
    public Entity CreatePlayerEntity()
    {
        Entity player = new Entity();
        player.AddComponent(new NameComponent(){name = "Player" + player.ID});
        player.AddComponent(new PlayerComponent());
        return player;
    }
    public void InitWorldPlayerEntity()
    {
        for (int i = 0; i < 2; i++)
        {
            World.Instance.AddEntity(CreatePlayerEntity());
        }
    }

    public override void Update()
    {
        Console.WriteLine("PlayerSystem Update");
        InitWorldPlayerEntity();
    }
}
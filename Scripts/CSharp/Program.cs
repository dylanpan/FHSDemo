using System;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\n---------->>> Init World");
        World world = World.Instance;
        
        Console.WriteLine("\n---------->>> Update World");
        world.AddSystem(new PlayerSystem());
        world.AddSystem(new BartenderSystem());
        world.AddSystem(new HeroPoolSystem());
        world.AddSystem(new PiecesPoolSystem());
        world.AddSystem(new HandCardSystem());
        world.AddSystem(new BattleCardSystem());
        world.AddSystem(new BattleAutoChessSystem());
        world.AddSystem(new BattleReplaySystem());
        world.Update();
        Console.WriteLine("----------<<<\n");
    }
}

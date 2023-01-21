public class LevelComponent: IComponent
{
    public int current_level;

    public override void tostring()
    {
        Console.WriteLine("---> LevelComponent:{current_level:" + current_level + "}");
    }
}
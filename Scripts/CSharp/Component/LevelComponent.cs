public class LevelComponent: IComponent
{
    private int _current_level = 0;
    public int current_level
    {
        get
        {
            return _current_level;
        }
        set
        {
            _current_level = value;
        }
    }

    public override void LoggerString()
    {
        Console.WriteLine("---> LevelComponent:{current_level:" + current_level + "}");
    }
}
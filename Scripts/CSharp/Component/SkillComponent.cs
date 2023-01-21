public class SkillComponent: IComponent
{
    private int _skill_id = -1;
    public int skill_id
    {
        get
        {
            return _skill_id;
        }
        set
        {
            _skill_id = value;
        }
    }
    public override void LoggerString()
    {
        Console.WriteLine("---> SkillComponent:{skill_id:" + skill_id + "}");
    }
}
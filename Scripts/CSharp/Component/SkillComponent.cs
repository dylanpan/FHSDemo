public class SkillComponent: IComponent
{
    public int skill_id = -1;
    public override void tostring()
    {
        Console.WriteLine("---> SkillComponent:{skill_id:" + skill_id + "}");
    }
}
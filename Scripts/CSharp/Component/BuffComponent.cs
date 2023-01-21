public class BuffComponent: IComponent
{
    public int buff_id = -1;
    public override void tostring()
    {
        Console.WriteLine("---> BuffComponent:{buff_id:" + buff_id + "}");
    }
}
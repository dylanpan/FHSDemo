public class BuffComponent: IComponent
{
    private int _buff_id = ConstUtil.None;
    public int buff_id
    {
        get
        {
            return _buff_id;
        }
        set
        {
            _buff_id = value;
        }
    }
    public override void LoggerString()
    {
        Console.WriteLine("---> BuffComponent:{buff_id:" + buff_id + "}");
    }
}
public class NameComponent: IComponent
{
    private string _name = ConstUtil.Empty;
    public string name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public override void LoggerString()
    {
        Console.WriteLine("---> NameComponent:{name:" + name + "}");
    }
}
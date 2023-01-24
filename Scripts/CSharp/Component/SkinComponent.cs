public class SkinComponent: IComponent
{
    private string _skin_name = ConstUtil.Empty;
    public string skin_name
    {
        get
        {
            return _skin_name;
        }
        set
        {
            _skin_name = value;
        }
    }
    
    public override void LoggerString()
    {
        Console.WriteLine("---> SkinComponent:{skin_name:" + skin_name + "}");
    }
}
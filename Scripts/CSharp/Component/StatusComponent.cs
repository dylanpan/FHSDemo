public class StatusComponent: IComponent
{
    // -1 - 无状态；1 - 冻结；2 - 攻击；3 - 死亡
    private int _status = -1;
    public int status
    {
        get
        {
            return _status;
        }
        set
        {
            _status = value;
        }
    }
    
    public override void LoggerString()
    {
        Console.WriteLine("---> StatusComponent:{status:" + status + "}");
    }
}
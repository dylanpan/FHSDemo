public class StatusComponent: IComponent
{
    // -1 - 无状态；0 - 冻结；1 - 待机；2 - 攻击；3 - 死亡；4 - 无法攻击
    private int _status = ConstUtil.None;
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
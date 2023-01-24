public class CurrencyComponent: IComponent
{
    private int _currency = ConstUtil.Zero;
    public int currency
    {
        get
        {
            return _currency;
        }
        set
        {
            _currency = value;
        }
    }
    private int _up_level_cost = ConstUtil.Zero;
    public int up_level_cost
    {
        get
        {
            return _up_level_cost;
        }
        set
        {
            _up_level_cost = value;
        }
    }
    private int _refresh_cost = ConstUtil.Zero;
    public int refresh_cost
    {
        get
        {
            return _refresh_cost;
        }
        set
        {
            _refresh_cost = value;
        }
    }
    private int _piece_cost = ConstUtil.Zero;
    public int piece_cost
    {
        get
        {
            return _piece_cost;
        }
        set
        {
            _piece_cost = value;
        }
    }
    private int _piece_recycle = ConstUtil.Zero;
    public int piece_recycle
    {
        get
        {
            return _piece_recycle;
        }
        set
        {
            _piece_recycle = value;
        }
    }
    public override void LoggerString()
    {
        Console.WriteLine("---> CurrencyComponent:{currency:" + currency + ", up_level_cost:" + up_level_cost + ", refresh_cost:" + refresh_cost + ", piece_cost:" + piece_cost + ", piece_recycle:" + piece_recycle + "}");
    }
}
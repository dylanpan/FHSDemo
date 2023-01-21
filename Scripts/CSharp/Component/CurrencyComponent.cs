public class CurrencyComponent: IComponent
{
    private int _currency = 0;
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
    private int _up_level_cost = 0;
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
    private int _refresh_cost = 0;
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
    private int _pieces_cost = 0;
    public int pieces_cost
    {
        get
        {
            return _pieces_cost;
        }
        set
        {
            _pieces_cost = value;
        }
    }
    private int _pieces_recycle = 0;
    public int pieces_recycle
    {
        get
        {
            return _pieces_recycle;
        }
        set
        {
            _pieces_recycle = value;
        }
    }
    public override void LoggerString()
    {
        Console.WriteLine("---> CurrencyComponent:{currency:" + currency + ", up_level_cost:" + up_level_cost + ", refresh_cost:" + refresh_cost + ", pieces_cost:" + pieces_cost + ", pieces_recycle:" + pieces_recycle + "}");
    }
}
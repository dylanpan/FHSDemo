public class PorpertyComponent: IComponent
{
    private int _atk = 0;
    public int atk
    {
        get
        {
            return _atk;
        }
        set
        {
            _atk = value;
        }
    }
    private int _hp = 0;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    private int _race = 0;
    public int race
    {
        get
        {
            return _race;
        }
        set
        {
            _race = value;
        }
    }
    public override void LoggerString()
    {
        Console.WriteLine("---> PorpertyComponent[" + this.GetHashCode() + "]:{atk:" + atk + ", hp: " + hp + ", race: " + race + "}");
    }
}
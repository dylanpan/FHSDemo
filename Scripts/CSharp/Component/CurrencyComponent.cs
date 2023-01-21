public class CurrencyComponent: IComponent
{
    public int currency = 0;
    public int up_level_cost = 0;
    public int refresh_cost = 0;
    public int pieces_cost = 0;
    public int pieces_recycle = 0;
    public override void tostring()
    {
        Console.WriteLine("---> CurrencyComponent:{currency:" + currency + ", up_level_cost:" + up_level_cost + ", refresh_cost:" + refresh_cost + ", pieces_cost:" + pieces_cost + ", pieces_recycle:" + pieces_recycle + "}");
    }
}
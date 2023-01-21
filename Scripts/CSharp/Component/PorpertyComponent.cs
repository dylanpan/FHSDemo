public class PorpertyComponent: IComponent
{
    public int atk = 0;
    public int hp = 0;
    public int race = 0;
    public override void tostring()
    {
        Console.WriteLine("---> PorpertyComponent:{atk:" + atk + ", hp: " + hp + ", race: " + race + "}");
    }
}
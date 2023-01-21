public class NameComponent: IComponent
{
    public string name = "";
    public override void tostring()
    {
        Console.WriteLine("---> NameComponent:{name:" + name + "}");
    }
}
public class SkinComponent: IComponent
{
    public string skin_name = "";
    
    public override void tostring()
    {
        Console.WriteLine("---> SkinComponent:{skin_name:" + skin_name + "}");
    }
}
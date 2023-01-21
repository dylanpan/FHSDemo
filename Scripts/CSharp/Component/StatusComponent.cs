public class StatusComponent: IComponent
{
    // -1 - 无状态；1 - 冻结；2 - 攻击；3 - 死亡
    public int status = -1;
    
    public override void tostring()
    {
        Console.WriteLine("---> StatusComponent:{status:" + status + "}");
    }
}
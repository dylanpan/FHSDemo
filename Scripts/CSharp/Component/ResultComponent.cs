public class ResultComponent: IComponent
{
    public Dictionary<int, Dictionary<int, List<Entity>>> resultDict = new Dictionary<int, Dictionary<int, List<Entity>>>();
    // 0 - 未结束，1 - 平，2 - 赢，3 - 输
    public int status = -1;
    public override void tostring()
    {
        Console.WriteLine("---> ResultComponent:{" + status + "}");
        foreach (KeyValuePair<int, Dictionary<int, List<Entity>>> kvp in resultDict)
        {
            Console.WriteLine("------round = {0}", kvp.Key);
            foreach (KeyValuePair<int, List<Entity>> item in kvp.Value)
            {
                Console.WriteLine("team = {0}", item.Key);
                Util.Battle_LoggerListPiecesEntity(item.Value);
            }
        }
    }
}
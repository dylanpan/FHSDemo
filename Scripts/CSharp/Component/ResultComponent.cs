public class ResultComponent: IComponent
{
    private Dictionary<int, Dictionary<int, List<Entity>>> _result_dict = new Dictionary<int, Dictionary<int, List<Entity>>>();
    public Dictionary<int, Dictionary<int, List<Entity>>> result_dict
    {
        get
        {
            return _result_dict;
        }
        set
        {
            _result_dict = value;
        }
    }
    // 0 - 未结束，1 - 平，2 - A赢，3 - B赢
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
        Console.WriteLine("---> ResultComponent:{" + status + "}");
        foreach (KeyValuePair<int, Dictionary<int, List<Entity>>> kvp in result_dict)
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
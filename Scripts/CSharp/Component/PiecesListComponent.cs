public class PiecesListComponent: IComponent
{
    public int max_num = -1;
    public int bartender_id = -1;
    public int hand_card_id = -1;
    public int battle_card_id = -1;
    public List<int> piecesIds = new List<int>();
    public override void tostring()
    {
        Console.WriteLine("---> PiecesListComponent:{piecesIds:[" + string.Join(",", piecesIds.ToArray()) + "], max_num: " + max_num + ", bartender_id: " + bartender_id + ", hand_card_id: " + hand_card_id + ", battle_card_id: " + battle_card_id + "}");
    }
}
public class PlayerComponent: IComponent
{
    public int hero_id = -1;
    public int bartender_id = -1;
    public int hand_card_id = -1;
    public int battle_card_id = -1;
    public override void tostring()
    {
        Console.WriteLine("---> PlayerComponent:{hero_id:" + hero_id + ", bartender_id: " + bartender_id + ", hand_card_id: " + hand_card_id + ", battle_card_id: " + battle_card_id + "}");
    }
}
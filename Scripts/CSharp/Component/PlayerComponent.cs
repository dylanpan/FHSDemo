public class PlayerComponent: IComponent
{
    private int _hero_id = -1;
    public int hero_id
    {
        get
        {
            return _hero_id;
        }
        set
        {
            _hero_id = value;
        }
    }
    private int _bartender_id = -1;
    public int bartender_id
    {
        get
        {
            return _bartender_id;
        }
        set
        {
            _bartender_id = value;
        }
    }
    private int _hand_card_id = -1;
    public int hand_card_id
    {
        get
        {
            return _hand_card_id;
        }
        set
        {
            _hand_card_id = value;
        }
    }
    private int _battle_card_id = -1;
    public int battle_card_id
    {
        get
        {
            return _battle_card_id;
        }
        set
        {
            _battle_card_id = value;
        }
    }
    public override void LoggerString()
    {
        Console.WriteLine("---> PlayerComponent:{hero_id:" + hero_id + ", bartender_id: " + bartender_id + ", hand_card_id: " + hand_card_id + ", battle_card_id: " + battle_card_id + "}");
    }
}
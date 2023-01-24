using System;

public class PiecesPoolSystem : ISystem
{
    public void GeneriatePieces(Dictionary<string, string> paramDict)
    {
        Entity pieces = new Entity();
        int _id = ConstUtil.Zero;
        int.TryParse(paramDict["id"], out _id);
        pieces.AddComponent(new NameComponent(){name = paramDict["name"], id = _id});
        pieces.AddComponent(new SkinComponent(){skin_name = paramDict["skin_name"]});
        int _current_level = ConstUtil.Zero;
        int.TryParse(paramDict["current_level"], out _current_level);
        pieces.AddComponent(new LevelComponent(){current_level = _current_level});
        int _pieces_cost = ConstUtil.Zero;
        int.TryParse(paramDict["pieces_cost"], out _pieces_cost);
        int _pieces_recycle = ConstUtil.Zero;
        int.TryParse(paramDict["pieces_recycle"], out _pieces_recycle);
        pieces.AddComponent(new CurrencyComponent(){pieces_cost = _pieces_cost, pieces_recycle = _pieces_recycle});
        int _atk = ConstUtil.Zero;
        int.TryParse(paramDict["atk"], out _atk);
        int _hp = ConstUtil.Zero;
        int.TryParse(paramDict["hp"], out _hp);
        int _race = ConstUtil.Zero;
        int.TryParse(paramDict["race"], out _race);
        pieces.AddComponent(new PorpertyComponent(){atk = _atk, hp = _hp, race = _race});
        pieces.AddComponent(new BuffComponent());
        pieces.AddComponent(new StatusComponent());
        World.Instance.AddEntity(pieces);
    }
    public void GeneriatePoolFormConfig()
    {
        List<Dictionary<string, string>> piecesConfigList = ConfigUtil.GetPiecesConfig();
        for (int i = 0; i < piecesConfigList.Count; i++)
        {
            Dictionary<string, string> configDict = piecesConfigList[i];
            Dictionary<string, string> paramDict = Util.GetCommonParamDict();
            foreach (KeyValuePair<string, string> config in configDict)
            {
                paramDict[config.Key] = config.Value;
            }
            GeneriatePieces(paramDict);
        }
    }

    public override void Update()
    {
        Console.WriteLine("PiecesPoolSystem Update");
        GeneriatePoolFormConfig();
    }
}
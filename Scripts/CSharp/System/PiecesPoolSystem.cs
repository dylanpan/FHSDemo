using System;

public class PiecesPoolSystem : ISystem
{
    public void GeneratePieceEntity(Dictionary<string, string> paramDict)
    {
        Entity piece = new Entity();
        int _id = ConstUtil.Zero;
        int.TryParse(paramDict["id"], out _id);
        piece.AddComponent(new NameComponent(){name = paramDict["name"], id = _id});
        piece.AddComponent(new SkinComponent(){skin_name = paramDict["skin_name"]});
        int _current_level = ConstUtil.Zero;
        int.TryParse(paramDict["current_level"], out _current_level);
        piece.AddComponent(new LevelComponent(){current_level = _current_level});
        int _piece_cost = ConstUtil.Zero;
        int.TryParse(paramDict["piece_cost"], out _piece_cost);
        int _piece_recycle = ConstUtil.Zero;
        int.TryParse(paramDict["piece_recycle"], out _piece_recycle);
        piece.AddComponent(new CurrencyComponent(){piece_cost = _piece_cost, piece_recycle = _piece_recycle});
        int _atk = ConstUtil.Zero;
        int.TryParse(paramDict["atk"], out _atk);
        int _hp = ConstUtil.Zero;
        int.TryParse(paramDict["hp"], out _hp);
        int _race = ConstUtil.Zero;
        int.TryParse(paramDict["race"], out _race);
        piece.AddComponent(new PorpertyComponent(){atk = _atk, hp = _hp, race = _race});
        piece.AddComponent(new BuffComponent());
        piece.AddComponent(new StatusComponent());
        World.Instance.AddEntity(piece);
    }
    public void GeneratePoolFormConfig()
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
            GeneratePieceEntity(paramDict);
        }
    }

    public override void Update()
    {
        Console.WriteLine("PiecesPoolSystem Update");
        GeneratePoolFormConfig();
    }
}
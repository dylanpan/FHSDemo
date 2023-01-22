
public class TestUtil
{
    public static Dictionary<string, string> GetCommonParamDict()
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        paramDict["name"] = "";
        paramDict["atk"] = "0";
        paramDict["hp"] = "0";
        return paramDict;
    }
    // TODO: PiecesPool 中生成棋子,然后从其中进行复制
    public static Entity GetTestPieces(Dictionary<string, string> paramDict)
    {
        Entity pieces = new Entity();
        pieces.AddComponent(new NameComponent(){name = paramDict["name"]});
        pieces.AddComponent(new SkinComponent(){skin_name = "pig_001"});
        pieces.AddComponent(new LevelComponent(){current_level = 1});
        pieces.AddComponent(new CurrencyComponent(){pieces_cost = 3, pieces_recycle = 1});
        int _atk = 0;
        int.TryParse(paramDict["atk"], out _atk);
        int _hp = 0;
        int.TryParse(paramDict["hp"], out _hp);
        pieces.AddComponent(new PorpertyComponent(){atk = _atk, hp = _hp, race = 4});
        pieces.AddComponent(new BuffComponent());
        pieces.AddComponent(new StatusComponent());
        World.Instance.AddEntity(pieces);
        return pieces;
    }
    public static void SetTestPiecesIds(ref Entity aEntity, ref Entity bEntity)
    {
        if (aEntity != null)
        {
            PiecesListComponent aPiecesListComponent = (PiecesListComponent)aEntity.GetComponent<PiecesListComponent>();
            if (aPiecesListComponent != null)
            {
                Dictionary<string, string> paramDict_0 = TestUtil.GetCommonParamDict();
                paramDict_0["name"] = "慵懒的野猪人";
                paramDict_0["atk"] = "0";
                paramDict_0["hp"] = "2";
                Entity pieces_0 = TestUtil.GetTestPieces(paramDict_0);
                Dictionary<string, string> paramDict_1 = TestUtil.GetCommonParamDict();
                paramDict_1["name"] = "慵懒的野猪人";
                paramDict_1["atk"] = "1";
                paramDict_1["hp"] = "2";
                Entity pieces_1 = TestUtil.GetTestPieces(paramDict_1);
                Dictionary<string, string> paramDict_2 = TestUtil.GetCommonParamDict();
                paramDict_2["name"] = "慵懒的野猪人";
                paramDict_2["atk"] = "4";
                paramDict_2["hp"] = "1";
                Entity pieces_2 = TestUtil.GetTestPieces(paramDict_2);
                int[] piecesIds = {pieces_0.ID, pieces_1.ID, pieces_2.ID};
                aPiecesListComponent.piecesIds = new List<int>(piecesIds);
            }
        }
        if (bEntity != null)
        {
            PiecesListComponent bPiecesListComponent = (PiecesListComponent)bEntity.GetComponent<PiecesListComponent>();
            if (bPiecesListComponent != null)
            {
                Dictionary<string, string> paramDict_0 = TestUtil.GetCommonParamDict();
                paramDict_0["name"] = "凶残的野猪人";
                paramDict_0["atk"] = "2";
                paramDict_0["hp"] = "2";
                Entity pieces_0 = TestUtil.GetTestPieces(paramDict_0);
                Dictionary<string, string> paramDict_1 = TestUtil.GetCommonParamDict();
                paramDict_1["name"] = "柔弱的野猪人";
                paramDict_1["atk"] = "1";
                paramDict_1["hp"] = "5";
                Entity pieces_1 = TestUtil.GetTestPieces(paramDict_1);
                Dictionary<string, string> paramDict_2 = TestUtil.GetCommonParamDict();
                paramDict_2["name"] = "柔弱的野猪人";
                paramDict_2["atk"] = "1";
                paramDict_2["hp"] = "5";
                Entity pieces_2 = TestUtil.GetTestPieces(paramDict_2);
                int[] piecesIds = {pieces_0.ID,pieces_1.ID,pieces_2.ID};
                bPiecesListComponent.piecesIds = new List<int>(piecesIds);
            }
        }
    }
}
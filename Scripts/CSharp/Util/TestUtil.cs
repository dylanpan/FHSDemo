
public class TestUtil
{
    // TODO: HeroPool 中生成英雄,然后从其中进行复制
    public static int GetTestPieces(int piece_id)
    {
        int find_id = 0;
        foreach (Entity entity in World.Instance.entityDic.Values)
        {
            if (Util.CheckIsPiece(entity))
            {
                NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
                if (nameComponent != null && nameComponent.id == piece_id)
                {
                    find_id = entity.ID;
                }
            }
        }
        return find_id;
    }
    public static void SetTestPiecesIds(ref Entity aEntity, ref Entity bEntity)
    {
        if (aEntity != null)
        {
            PiecesListComponent aPiecesListComponent = (PiecesListComponent)aEntity.GetComponent<PiecesListComponent>();
            if (aPiecesListComponent != null)
            {
                int pieces_0_ID = TestUtil.GetTestPieces(4001);
                int pieces_1_ID = TestUtil.GetTestPieces(4002);
                int pieces_2_ID = TestUtil.GetTestPieces(4003);
                int[] piecesIds = {pieces_0_ID, pieces_1_ID, pieces_2_ID};
                aPiecesListComponent.piecesIds = new List<int>(piecesIds);
            }
        }
        if (bEntity != null)
        {
            PiecesListComponent bPiecesListComponent = (PiecesListComponent)bEntity.GetComponent<PiecesListComponent>();
            if (bPiecesListComponent != null)
            {
                int pieces_0_ID = TestUtil.GetTestPieces(4004);
                int pieces_1_ID = TestUtil.GetTestPieces(4005);
                int pieces_2_ID = TestUtil.GetTestPieces(4005);
                int[] piecesIds = {pieces_0_ID, pieces_1_ID, pieces_2_ID};
                bPiecesListComponent.piecesIds = new List<int>(piecesIds);
            }
        }
    }

    public static void AddBartenderComponents(Entity entity)
    {
        if (entity.GetComponent<NameComponent>() == null)
        {
            entity.AddComponent(new NameComponent(){name = "调酒机器人"});
        }
        if (entity.GetComponent<CurrencyComponent>() == null)
        {
            entity.AddComponent(new CurrencyComponent(){currency = 3, up_level_cost = 4, refresh_cost = 1});
        }
        if (entity.GetComponent<LevelComponent>() == null)
        {
            entity.AddComponent(new LevelComponent(){current_level = 1});
        }
        if (entity.GetComponent<SkinComponent>() == null)
        {
            entity.AddComponent(new SkinComponent(){skin_name = "wine_001"});
        }
        if (entity.GetComponent<PiecesListComponent>() == null)
        {
            entity.AddComponent(new PiecesListComponent(){max_num = 3, bartender_id = entity.ID});
        }
    }

    public static void AddHeroComponents(Entity entity)
    {
        if (entity.GetComponent<NameComponent>() == null)
        {
            entity.AddComponent(new NameComponent(){name = "迦拉克隆"});
        }
        if (entity.GetComponent<SkinComponent>() == null)
        {
            entity.AddComponent(new SkinComponent(){skin_name = "jk_001"});
        }
        if (entity.GetComponent<SkillComponent>() == null)
        {
            entity.AddComponent(new SkillComponent(){skill_id = 1001});
        }
        if (entity.GetComponent<PorpertyComponent>() == null)
        {
            entity.AddComponent(new PorpertyComponent(){atk = 0, hp = 40});
        }
    }
}
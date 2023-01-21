using System;

public class BattleCardSystem : ISystem
{
    public Entity CreateBattleCardEntity()
    {
        Entity entity = new Entity();
        if (entity.GetComponent<NameComponent>() == null)
        {
            entity.AddComponent(new NameComponent(){name = "BattleCard"});
        }
        if (entity.GetComponent<PiecesListComponent>() == null)
        {
            entity.AddComponent(new PiecesListComponent(){piecesIds = new List<int>(GetTestRandomPiecesIds()), max_num = 7, battle_card_id = entity.ID});
        }
        return entity;
    }
    public Entity GetTestPieces()
    {
        Entity pieces = new Entity();
        pieces.AddComponent(new NameComponent(){name = "慵懒的野猪人"});
        pieces.AddComponent(new SkinComponent(){skin_name = "pig_001"});
        pieces.AddComponent(new LevelComponent(){current_level = 1});
        pieces.AddComponent(new CurrencyComponent(){pieces_cost = 3, pieces_recycle = 1});
        pieces.AddComponent(new PorpertyComponent(){atk = 1, hp = 2, race = 4});
        pieces.AddComponent(new BuffComponent());
        pieces.AddComponent(new StatusComponent());
        World.Instance.AddEntity(pieces);
        return pieces;
    }
    public int[] GetTestRandomPiecesIds()
    {
        Entity pieces = GetTestPieces();
        int[] piecesIds = {pieces.ID};
        return piecesIds;
    }
    public void CheckAddBattleCardEntity(ref Entity entity)
    {
        PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
        if (playerComponent != null)
        {
            if (playerComponent.battle_card_id == -1)
            {
                Entity battleCard = CreateBattleCardEntity();
                playerComponent.battle_card_id = battleCard.ID;
                World.Instance.AddEntity(battleCard);
            }
        }
    }
    public override void Update()
    {
        Console.WriteLine("BattleCardSystem Update");
        for (int i = 0; i < World.Instance.entityDic.Values.Count; i++)
        {
            Entity entity = World.Instance.entityDic.Values.ElementAt(i);
            CheckAddBattleCardEntity(ref entity);
        }
    }
}
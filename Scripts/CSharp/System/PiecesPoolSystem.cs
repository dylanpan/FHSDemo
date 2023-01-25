using System;

public class PiecesPoolSystem : ISystem
{
    public void GeneratePieceEntity(PiecesConfig piecesConfig)
    {
        if (piecesConfig != null)
        {
            Entity entity = new Entity();
            entity.AddComponent(new NameComponent(){name = piecesConfig.name, id = piecesConfig.id});
            entity.AddComponent(new SkinComponent(){skin_name = piecesConfig.skin_name});
            entity.AddComponent(new LevelComponent(){current_level = piecesConfig.current_level});
            entity.AddComponent(new CurrencyComponent(){piece_cost = piecesConfig.piece_cost, piece_recycle = piecesConfig.piece_recycle});
            entity.AddComponent(new PorpertyComponent(){atk = piecesConfig.atk, hp = piecesConfig.hp, race = piecesConfig.race});
            entity.AddComponent(new BuffComponent());
            entity.AddComponent(new StatusComponent());
            World.Instance.AddEntity(entity);
        }
    }

    public void GeneratePoolFormConfig()
    {
        List<PiecesConfig> configDataList = ConfigUtil.GetConfigData<PiecesConfig>(ConstUtil.JsonFile_PiecesConfig);
        for (int i = 0; i < configDataList.Count; i++)
        {
            PiecesConfig piecesConfig = configDataList[i];
            GeneratePieceEntity(piecesConfig);
        }
    }

    public override void Update()
    {
        Console.WriteLine("PiecesPoolSystem Update");
        GeneratePoolFormConfig();
    }
}
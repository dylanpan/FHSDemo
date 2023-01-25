using System;
using Chess.Base;
using Chess.Config;
using Chess.Component;
using Chess.Util;

namespace Chess.System
{
    public class BattleCardSystem : ISystem
    {
        public Entity CreateBattleCardEntity()
        {
            Entity entity = new Entity();
            entity.AddComponent(new NameComponent(){name = "BattleCard"});
            entity.AddComponent(new PiecesListComponent(){max_num = ConstUtil.Max_BattleCardNum, battle_card_id = entity.ID});
            return entity;
        }
        public void CheckAddBattleCardEntity(ref Entity entity)
        {
            PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
            if (playerComponent != null)
            {
                if (playerComponent.battle_card_id == ConstUtil.None)
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
}
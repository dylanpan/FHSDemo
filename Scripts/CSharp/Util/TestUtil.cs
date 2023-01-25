using Chess.Base;
using Chess.Component;

namespace Chess.Util
{
    public class TestUtil
    {
        public static int GetTestPieces(int piece_id)
        {
            int find_id = 0;
            foreach (Entity entity in World.Instance.entityDic.Values)
            {
                if (CommonUtil.CheckIsPiece(entity))
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

        public static Entity GetTestHero()
        {
            Entity? heroEntity = null;
            foreach (Entity entity in World.Instance.entityDic.Values)
            {
                if (CommonUtil.CheckIsHero(entity))
                {
                    NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
                    StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                    if (nameComponent != null && statusComponent != null && statusComponent.status != ConstUtil.Status_HeroPick)
                    {
                        statusComponent.status = ConstUtil.Status_HeroPick;
                        heroEntity = entity;
                        break;
                    }
                }
            }
            return heroEntity;
        }

        public static void SetHero()
        {
            foreach (Entity entity in World.Instance.entityDic.Values)
            {
                PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    if (playerComponent.hero_id == ConstUtil.None)
                    {
                        Entity hero = TestUtil.GetTestHero();
                        playerComponent.hero_id = hero.ID;
                    }
                }
            }
        }

        public static Entity GetTestBartender()
        {
            Entity? bartenderEntity = null;
            foreach (Entity entity in World.Instance.entityDic.Values)
            {
                    NameComponent nameComponent1 = (NameComponent)entity.GetComponent<NameComponent>();
                if (CommonUtil.CheckIsBartender(entity))
                {
                    NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
                    StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                    if (nameComponent != null && statusComponent != null && statusComponent.status != ConstUtil.Status_BartenderPick)
                    {
                        statusComponent.status = ConstUtil.Status_BartenderPick;
                        bartenderEntity = entity;
                        break;
                    }
                }
            }
            return bartenderEntity;
        }
        public static void SetBartender()
        {
            for (int i = 0; i < World.Instance.entityDic.Values.Count; i++)
            {
                Entity entity = World.Instance.entityDic.Values.ElementAt(i);
                PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    if (playerComponent.bartender_id == ConstUtil.None)
                    {
                        Entity bartender = TestUtil.GetTestBartender();
                        playerComponent.bartender_id = bartender.ID;
                    }
                }
            }
        }
    }
}
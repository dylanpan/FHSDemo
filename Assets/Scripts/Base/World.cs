using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Systems;
using Chess.Util;

namespace Chess.Base
{
    public class World
    {
        private static World? instance = null;
        
        public static World Instance
        {
            get { return instance != null ? instance : (instance = new World()); }
        }

        public List<ISystem> systems = new List<ISystem>();
        public Dictionary<int, Entity> entityDic = new Dictionary<int, Entity>();

        public void AddSystem(ISystem system)
        {
            if (!HaveSystem(system))
            {
                systems.Add(system);
            }
        }
        public void RemoveSystem(ISystem system)
        {
            if (HaveSystem(system))
            {
                systems.Remove(system);
            }
        }
        public bool HaveSystem(ISystem system)
        {
            return systems.Contains(system);
        }

        public void AddEntity(Entity entity)
        {
            if (!HaveEntity(entity))
            {
                entityDic.Add(entity.ID, entity);
            }
        }
        public void RemoveEntity(Entity entity)
        {
            if (HaveEntity(entity))
            {
                entityDic.Remove(entity.ID);
            }
        }
        public bool HaveEntity(Entity entity)
        {
            return entityDic.ContainsKey(entity.ID);
        }
        public void AddSystem()
        {
            World.Instance.AddSystem(new AISystem());
            World.Instance.AddSystem(new PlayerSystem());
            World.Instance.AddSystem(new BartenderSystem());
            World.Instance.AddSystem(new HeroPoolSystem());
            World.Instance.AddSystem(new PiecesPoolSystem());
            World.Instance.AddSystem(new HandCardSystem());
            World.Instance.AddSystem(new BattleCardSystem());
            World.Instance.AddSystem(new MatchSystem());
            World.Instance.AddSystem(new BattleAutoChessSystem());
            World.Instance.AddSystem(new BattleReplaySystem());
            World.Instance.AddSystem(new ViewSystem());
        }
        // ???????????????
        public void Update()
        {
            if (Process.GetInstance().CheckProcessIsEqual(Process.GetInstance().GetShowPlayerId(), ConstUtil.None))
            {
                AddSystem();
            }
            foreach (ISystem system in systems)
            {
                system.Update();
            }
        }
    }
}

// TODO: ????????????????????????????????????????????????????????????????????????????????????
// TODO: ??????????????????????????? ID???????????????????????? AI ID ????????????
// TODO: ????????????????????? AI ????????????????????????
// TODO: ??? AI ??????????????????????????????????????????????????????????????????
// TODO: BattleReplaySystem ?????????????????????????????????????????????????????????????????? ID ????????????
// TODO: AI ????????????????????????

// ??????????????????????????????????????????????????? GameStateMachine

// World
// {
//     Player
//     {
//         name
//         Hero
//         {
//             name
//             skin
//             skill
//             hp
//             atk
//         }
//         Bartender
//         {
//             name
//             currency
//             level
//             skin
//             piecesList
//             {
//                 maxNum
//                 Pieces
//                 {
//                     name
//                     skin
//                     level
//                     race
//                     cost
//                     recycle
//                     buff
//                     hp
//                     atk
//                     status
//                 }
//             }
//         }
//         HandCard
//         {
//             piecesList
//             {
//                 maxNum
//                 Pieces
//                 {
//                     name
//                     cost
//                     receive
//                     skin
//                     level
//                     race
//                     buff
//                     hp
//                     atk
//                     status
//                 }
//             }
//         }
//         BattleCard
//         {
//             piecesList
//             {
//                 maxNum
//                 Pieces
//                 {
//                     name
//                     cost
//                     receive
//                     skin
//                     level
//                     race
//                     buff
//                     hp
//                     atk
//                     status
//                 }
//             }
//         }
//     }

//     Pool
//     {
//         HeroList
//         {
//             Hero
//             {
//                 name
//                 skin
//                 skill
//                 hp
//                 atk
//             }
//         }
//         PiecesList
//         {
//             Pieces
//             {
//                 name
//                 cost
//                 receive
//                 skin
//                 level
//                 race
//                 buff
//                 hp
//                 atk
//                 status
//             }
//         }
//     }
// }
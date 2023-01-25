using System;
using System.Collections.Generic;
using System.Linq;

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
        // 由引擎驱动
        public void Update()
        {
            foreach (ISystem system in systems)
            {
                system.Update();
            }
            foreach (Entity entity in entityDic.Values)
            {
                Console.WriteLine("\n------>>>Entity ID: " + entity.ID);
                foreach (IComponent component in entity.components)
                {
                    component.LoggerString();
                }
            }
        }
    }
}


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
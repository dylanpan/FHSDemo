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
            World.Instance.AddSystem(new PlayerSystem());
            World.Instance.AddSystem(new BartenderSystem());
            World.Instance.AddSystem(new HeroPoolSystem());
            World.Instance.AddSystem(new PiecesPoolSystem());
            World.Instance.AddSystem(new HandCardSystem());
            World.Instance.AddSystem(new BattleCardSystem());
            World.Instance.AddSystem(new BattleAutoChessSystem());
            World.Instance.AddSystem(new BattleReplaySystem());
            World.Instance.AddSystem(new ViewSystem());
            World.Instance.AddSystem(new AISystem());
        }
        // 由引擎驱动
        public void Update()
        {
            if (Process.GetInstance().GetProcess(Process.GetInstance().GetShowPlayerId()) == ConstUtil.None)
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

// TODO: 准备时间的倒计时结束切换到战斗状态，可先通过按钮进行切换
// TODO: 目前使用的全局玩家 ID，对应后台运行的 AI ID 怎么使用
// TODO: 准备阶段需补充 AI 进行后台准备操作
// TODO: 无 AI 的情况下可以提前编辑好战斗队伍，直接进行战斗
// TODO: BattleReplaySystem 该系统中回放已经进行对战得到结果的对局，通过 ID 进行切换

// 整个游戏启动包装一层状态监测，参考 GameStateMachine

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
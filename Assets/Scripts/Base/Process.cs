using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Config;
using Chess.Component;
using Chess.Util;
using UnityEngine;

namespace Chess.Base
{
    // 存储当前游戏使用数据
    public class Process
    {
        #region 单例 - 不考虑线程安全
        private static Process? no_save_instance = null;
        
        public static Process Instance
        {
            get { return no_save_instance != null ? no_save_instance : (no_save_instance = new Process()); }
        }
        #endregion
        #region 单例 - C# 独有写法
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly Process inner_instance = new Process();
        }
        public static Process _Instance { get { return Nested.inner_instance; } }
        #endregion
        #region 单例 - 考虑线程安全
        private volatile static Process _instance = null;
        private static readonly object lockHelper = new object();
        private Process()
        {
        }
        public static Process GetInstance()
        {
            if (_instance == null)
            {
                lock(lockHelper)
                {
                    if (_instance == null)
                    {
                        _instance = new Process();
                    }
                }
            }
            return _instance;
        }
        #endregion

        /// <summary>
        /// 开启游戏界面进行设置玩家与 AI 的类型数量
        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <returns></returns>
        private List<int> _player_type_list = new List<int>();
        public List<int> GetPlayerTypeList()
        {
            return _player_type_list;
        }
        public void SetPlayerTypeList(int player_type)
        {
            _player_type_list.Add(player_type);
        }

        /// <summary>
        /// 游戏内的玩家 ID 列表
        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <returns></returns>
        private List<int> _player_id_list = new List<int>();
        public List<int> GetPlayerIdList()
        {
            return _player_id_list;
        }
        public void SetPlayerIdList(int player_id)
        {
            _player_id_list.Add(player_id);
        }

        /// <summary>
        /// 对应玩家 ID 对应的进度
        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <typeparam name="int"></typeparam>
        /// <returns></returns>
        private Dictionary<int, int> _current_process_dict = new Dictionary<int, int>();
        public void SetProcess(int process, int player_id)
        {
            _current_process_dict[player_id] = process;
        }
        public int GetProcess(int player_id)
        {
            if (!_current_process_dict.ContainsKey(player_id))
            {
                return ConstUtil.None;
            }
            return _current_process_dict[player_id];
        }
        public bool CheckProcessIsEqual(int player_id, int check_process)
        {
            int player_process = GetProcess(player_id);
            bool isEqual = player_process == check_process;
            return isEqual;
        }

        /// <summary>
        /// /// 当前展示玩家
        /// </summary>
        private int _show_player_id = ConstUtil.None;
        public void SetShowPlayerId(int id)
        {
            _show_player_id = id;
        }
        public int GetShowPlayerId()
        {
            return _show_player_id;
        }

        /// <summary>
        /// 英雄池子
        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <returns></returns>
        private List<int> _hero_pool = new List<int>();
        public void AddHeroToPool(int id)
        {
            _hero_pool.Add(id);
        }
        public void RemoveHeroFormPool(int id)
        {
            if (_hero_pool.Contains(id))
            {
                _hero_pool.Remove(id);
            }
        }
        public List<int> GetHeroPool()
        {
            return _hero_pool;
        }

        /// <summary>
        /// 酒馆池子
        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <returns></returns>
        private List<int> _bartender_pool = new List<int>();
        public void AddBartenderToPool(int id)
        {
            _bartender_pool.Add(id);
        }
        public void RemoveBartenderFormPool(int id)
        {
            if (_bartender_pool.Contains(id))
            {
                _bartender_pool.Remove(id);
            }
        }
        public List<int> GetBartenderPool()
        {
            return _bartender_pool;
        }

        /// <summary>
        /// 提供给玩家选取英雄列表
        /// </summary>
        /// <returns></returns>
        private Dictionary<int,List<int>> _player_hero_list_dict = new Dictionary<int,List<int>>();
        public void AddHeroListToDict(int player_id, List<int> hero_list)
        {
            _player_hero_list_dict.Add(player_id, hero_list);
        }
        public List<int> GetHeroPoolFormDict(int player_id)
        {
            List<int> hero_list = new List<int>();
            if (_player_hero_list_dict.ContainsKey(player_id))
            {
                hero_list = _player_hero_list_dict[player_id];
            }
            return hero_list;
        }

        /// <summary>
        /// 对应等级的棋子池子
        /// </summary>
        /// <returns></returns>
        private Dictionary<int,List<int>> _pieces_pool_dict = new Dictionary<int,List<int>>();
        public void AddPiecePoolToDict(int level, int piece_id)
        {
            if (_pieces_pool_dict.ContainsKey(level))
            {
                List<int> piece_pool = _pieces_pool_dict[level];
                piece_pool.Add(piece_id);
            }
            else
            {
                List<int> piece_pool = new List<int>();
                piece_pool.Add(piece_id);
                _pieces_pool_dict.Add(level, piece_pool);
            }
        }
        public List<int> GetPiecePoolFormDict(int level)
        {
            List<int> piece_pool = new List<int>();
            if (level > 0)
            {
                for (int i = 1; i <= level; i++)
                {
                    if (_pieces_pool_dict.ContainsKey(i))
                    {
                        List<int> tmp_piece_pool = _pieces_pool_dict[i];
                        piece_pool = piece_pool.Concat(tmp_piece_pool).ToList<int>();
                    }
                }
            }
            return piece_pool;
        }

        /// <summary>
        /// 当前 BartenderView 操作页面的记录
        /// </summary>
        private bool _isFreeze = false;
        public void SetBartenderPieceFreezeState(bool state)
        {
            _isFreeze = state;
        }
        public bool GetBartenderPieceFreezeState()
        {
            return _isFreeze;
        }
    }
}
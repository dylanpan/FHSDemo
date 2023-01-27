using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Config;
using Chess.Component;
using Chess.Util;
using UnityEngine;

namespace Chess.Base
{
    public class Process
    {
        private static Process? instance = null;
        
        public static Process Instance
        {
            get { return instance != null ? instance : (instance = new Process()); }
        }
        private int _current_process = ConstUtil.None;

        public void SetProcess(int process)
        {
            _current_process = process;
        }

        public int GetProcess()
        {
            return _current_process;
        }

        private int _current_level = ConstUtil.Init_Level;
        public void SetCurrentLevel(int level)
        {
            _current_level = level;
        }

        public int GetCurrentLevel()
        {
            return _current_level;
        }
        private int _current_currency = ConstUtil.Init_Currency;
        public void SetCurrentCurrency(int currency)
        {
            _current_currency = currency;
        }

        public int GetCurrentCurrency()
        {
            return _current_currency;
        }
        private int _self_player_id = ConstUtil.None;

        public void SetSelfPlayerId(int id)
        {
            _self_player_id = id;
        }

        public int GetSelfPlayerId()
        {
            return _self_player_id;
        }

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
        private int _bartender_id = ConstUtil.None;

        public void SetBartenderId(int id)
        {
            _bartender_id = id;
        }

        public int GetBartenderId()
        {
            return _bartender_id;
        }
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

        private Dictionary<int,List<int>> _player_hero_pool_dict = new Dictionary<int,List<int>>();
        public void AddHeroPoolToDict(int player_id, List<int> hero_pool)
        {
            _player_hero_pool_dict.Add(player_id, hero_pool);
        }
        public List<int> GetHeroPoolFormDict(int player_id)
        {
            List<int> hero_pool = new List<int>();
            if (_player_hero_pool_dict.ContainsKey(player_id))
            {
                hero_pool = _player_hero_pool_dict[player_id];
            }
            return hero_pool;
        }
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
    }
}
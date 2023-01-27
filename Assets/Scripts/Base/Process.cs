using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Config;
using Chess.Component;
using Chess.Util;

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
        public void AddHeroPoolToDict(int id, List<int> hero_pool)
        {
            _player_hero_pool_dict.Add(id, hero_pool);
        }
        public List<int> GetHeroPoolFormDict(int id)
        {
            List<int> hero_pool = new List<int>();
            if (_player_hero_pool_dict.ContainsKey(id))
            {
                hero_pool = _player_hero_pool_dict[id];
            }
            return hero_pool;
        }
    }
}
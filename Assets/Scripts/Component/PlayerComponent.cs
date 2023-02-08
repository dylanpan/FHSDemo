using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Util;
using UnityEngine;

namespace Chess.Component
{
    public class PlayerComponent: IComponent
    {
        private int _hero_id = ConstUtil.None;
        public int hero_id
        {
            get
            {
                return _hero_id;
            }
            set
            {
                _hero_id = value;
            }
        }
        private int _bartender_id = ConstUtil.None;
        public int bartender_id
        {
            get
            {
                return _bartender_id;
            }
            set
            {
                _bartender_id = value;
            }
        }
        private int _hand_card_id = ConstUtil.None;
        public int hand_card_id
        {
            get
            {
                return _hand_card_id;
            }
            set
            {
                _hand_card_id = value;
            }
        }
        private int _battle_card_id = ConstUtil.None;
        public int battle_card_id
        {
            get
            {
                return _battle_card_id;
            }
            set
            {
                _battle_card_id = value;
            }
        }
        private int _piece_buy_id = ConstUtil.None;
        public int piece_buy_id
        {
            get
            {
                return _piece_buy_id;
            }
            set
            {
                _piece_buy_id = value;
            }
        }
        private int _piece_sell_id = ConstUtil.None;
        public int piece_sell_id
        {
            get
            {
                return _piece_sell_id;
            }
            set
            {
                _piece_sell_id = value;
            }
        }
        private int _piece_move_source_id = ConstUtil.None;
        public int piece_move_source_id
        {
            get
            {
                return _piece_move_source_id;
            }
            set
            {
                _piece_move_source_id = value;
            }
        }
        private int _piece_move_target_id = ConstUtil.None;
        public int piece_move_target_id
        {
            get
            {
                return _piece_move_target_id;
            }
            set
            {
                _piece_move_target_id = value;
            }
        }
        private int _ai_id = ConstUtil.None;
        public int ai_id
        {
            get
            {
                return _ai_id;
            }
            set
            {
                _ai_id = value;
            }
        }
        public override void LoggerString()
        {
            Debug.Log("---> PlayerComponent:{"
                         + "hero_id:" + hero_id
                         + ", bartender_id: " + bartender_id
                         + ", hand_card_id: " + hand_card_id
                         + ", battle_card_id: " + battle_card_id 
                         + ", piece_buy_id: " + piece_buy_id 
                         + ", piece_sell_id: " + piece_sell_id
                         + ", piece_move_source_id: " + piece_move_source_id
                         + ", piece_move_target_id: " + piece_move_target_id
                         + ", ai_id: " + ai_id
                         + "}");
        }
    }
}
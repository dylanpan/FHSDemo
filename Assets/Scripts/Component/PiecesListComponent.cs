using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Util;
using UnityEngine;

namespace Chess.Component
{
    public class PiecesListComponent: IComponent
    {
        private int _max_num = ConstUtil.None;
        public int max_num
        {
            get
            {
                return _max_num;
            }
            set
            {
                _max_num = value;
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
        public List<int> piecesIds = new List<int>();
        public override void LoggerString()
        {
            Debug.Log("---> PiecesListComponent:{piecesIds:[" + string.Join(",", piecesIds.ToArray()) + "], max_num: " + max_num + ", bartender_id: " + bartender_id + ", hand_card_id: " + hand_card_id + ", battle_card_id: " + battle_card_id + "}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Util;
using UnityEngine;

namespace Chess.Component
{
    public class BuffComponent: IComponent
    {
        private int _buff_id = ConstUtil.None;
        public int buff_id
        {
            get
            {
                return _buff_id;
            }
            set
            {
                _buff_id = value;
            }
        }
        public override void LoggerString()
        {
            Debug.Log("---> BuffComponent:{buff_id:" + buff_id + "}");
        }
    }
}
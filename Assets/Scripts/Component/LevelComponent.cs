using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Util;
using UnityEngine;

namespace Chess.Component
{
    public class LevelComponent: IComponent
    {
        private int _current_level = ConstUtil.Zero;
        public int current_level
        {
            get
            {
                return _current_level;
            }
            set
            {
                _current_level = value;
            }
        }

        public override void LoggerString()
        {
            Debug.Log("---> LevelComponent:{current_level:" + current_level + "}");
        }
    }
}
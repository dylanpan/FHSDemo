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
        private int _level = ConstUtil.Zero;
        public int level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        public override void LoggerString()
        {
            Debug.Log("---> LevelComponent:{level:" + level + "}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Util;
using UnityEngine;

namespace Chess.Component
{
    public class SkillComponent: IComponent
    {
        private int _skill_id = ConstUtil.None;
        public int skill_id
        {
            get
            {
                return _skill_id;
            }
            set
            {
                _skill_id = value;
            }
        }
        public override void LoggerString()
        {
            Debug.Log("---> SkillComponent:{skill_id:" + skill_id + "}");
        }
    }
}
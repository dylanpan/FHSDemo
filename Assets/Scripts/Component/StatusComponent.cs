using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Util;
using UnityEngine;

namespace Chess.Component
{
    public class StatusComponent: IComponent
    {
        // 通常: -1 - 无状态；
        // 棋子酒馆: 0 - 冻结；5 - 被选择中
        // 棋子战斗: 1 - 待机；2 - 攻击；3 - 死亡；4 - 无法攻击
        // 英雄: 6 - 被选择中；7 - 死亡
        private int _status = ConstUtil.None;
        public int status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        
        public override void LoggerString()
        {
            Debug.Log("---> StatusComponent:{status:" + status + "}");
        }
    }
}
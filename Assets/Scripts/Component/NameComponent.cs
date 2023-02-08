using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Util;
using UnityEngine;

namespace Chess.Component
{
    public class NameComponent: IComponent
    {
        private string _name = ConstUtil.Empty;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private int _id = ConstUtil.Zero;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private int _belong = ConstUtil.None;
        public int belong
        {
            get
            {
                return _belong;
            }
            set
            {
                _belong = value;
            }
        }
        public override void LoggerString()
        {
            Debug.Log("---> NameComponent:{name:" + name + ", id:" + id + ", belong:" + belong + "}");
        }
    }
}
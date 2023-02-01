using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Util;
using UnityEngine;

namespace Chess.Component
{
    public class ConfigComponent<T>: IComponent
    {
        private T _config;
        public T config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
            }
        }
        public override void LoggerString()
        {
            Debug.Log("---> ConfigComponent<T>:{config:" + typeof(config) + "}");
        }
    }
}
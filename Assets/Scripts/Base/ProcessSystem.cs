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
        public int current_process = ConstUtil.None;

        public int current_level = ConstUtil.Init_Level;
        public int current_currency = ConstUtil.Init_Currency;
        // TODO: 在各个系统中进行判断相关进度,执行对应结果
    }
}
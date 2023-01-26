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
        private int _current_process = ConstUtil.None;

        public int current_level = ConstUtil.Init_Level;
        public int current_currency = ConstUtil.Init_Currency;

        public void SetProcess(int process)
        {
            _current_process = process;
        }

        public int GetProcess()
        {
            return _current_process;
        }
        // TODO: 在各个系统中进行判断相关进度,执行对应结果
    }
}
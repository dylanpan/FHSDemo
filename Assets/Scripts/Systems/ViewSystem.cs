using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Config;
using Chess.Component;
using Chess.Util;
using UnityEngine;

namespace Chess.Systems
{
    public class ViewSystem : ISystem
    {
        public void AddGameMainView()
        {
            GameObject mainView = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/MainView"));
            GameObject mUICanvas = GameObject.Find("Canvas");
            mainView.transform.parent = mUICanvas.transform;
        }
        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.None)
            {
                Debug.Log("ViewSystem Update - init");
                AddGameMainView();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Main_View);
            }
        }
    }
}
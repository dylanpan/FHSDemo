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
            GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/MainView"));
            GameObject mUICanvas = GameObject.Find("UIRoot");
            view.transform.parent = mUICanvas.transform;
        }
        public void AddHeroPickView()
        {
            GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/HeroPickView"));
            GameObject mUICanvas = GameObject.Find("UIRoot");
            view.transform.parent = mUICanvas.transform;
        }
        public void AddBartenderView()
        {
            GameObject bartenderView = GameObject.Find("UIRoot/BartenderView");
            if (bartenderView == null)
            {
                Debug.Log("ViewSystem Update - add bartender");
                GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BartenderView"));
                GameObject mUICanvas = GameObject.Find("UIRoot");
                view.name = "BartenderView";
                view.transform.parent = mUICanvas.transform;
                BartenderView script = view.transform.GetComponent<BartenderView>();
                script.UpdateViewByData();
                Process.Instance.SetProcess(ConstUtil.Process_Prepare_Bartender_Refresh_Pre);
            }
        }
        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.None)
            {
                Debug.Log("ViewSystem Update - init");
                AddGameMainView();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Main_View);
            }
            else if (Process.Instance.GetProcess() == ConstUtil.Process_Pick_Hero)
            {
                Debug.Log("ViewSystem Update - pick hero");
                AddHeroPickView();
                Process.Instance.SetProcess(ConstUtil.Process_Pick_Hero_Ing);
            }
            else if (Process.Instance.GetProcess() == ConstUtil.Process_Prepare_Start)
            {
                AddBartenderView();
            }
        }
    }
}
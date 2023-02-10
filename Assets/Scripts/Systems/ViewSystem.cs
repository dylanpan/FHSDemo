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
            }
        }
        public void AddPlayerView()
        {
            GameObject playerView = GameObject.Find("UIRoot/PlayerView");
            if (playerView == null)
            {
                Debug.Log("ViewSystem Update - add bartender");
                GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/PlayerView"));
                GameObject mUICanvas = GameObject.Find("UIRoot");
                view.name = "PlayerView";
                view.transform.parent = mUICanvas.transform;
                PlayerView script = view.transform.GetComponent<PlayerView>();
                script.UpdateViewByData();
            }
        }
        public override void Update()
        {
            if (Process.GetInstance().GetProcess(Process.GetInstance().GetShowPlayerId()) == ConstUtil.Process_Pick_Hero)
            {
                Debug.Log("ViewSystem Update - pick hero");
                AddHeroPickView();
                Process.GetInstance().SetProcess(ConstUtil.Process_Pick_Hero_Ing, Process.GetInstance().GetShowPlayerId());
            }
            else if (Process.GetInstance().GetProcess(Process.GetInstance().GetShowPlayerId()) == ConstUtil.Process_Prepare_Start)
            {
                AddBartenderView();
                AddPlayerView();
                Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Bartender_Refresh_Pre, Process.GetInstance().GetShowPlayerId());
            }
            else if (Process.GetInstance().GetProcess(Process.GetInstance().GetShowPlayerId()) == ConstUtil.Process_Prepare_Switch)
            {
                // TODO: - 1 需要兼容在切换不同玩家的操作（补充准备阶段切换状态和战斗阶段切换转状态，专门在这个状态下进行操作）
            }
        }
    }
}
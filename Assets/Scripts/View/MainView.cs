using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Chess.Base;
using Chess.Util;

public class MainView : MonoBehaviour
{
    public Button StartBtn;
    public Button LoadBtn;
    public Button SettingBtn;

    private bool isLoadEnd = false;
    
    // Start is called before the first frame update
    void Start()
    {
        InitView();
    }

    public void InitView()
    {
        Debug.Log("MainView InitView");
        StartBtn.onClick.AddListener(OnClickStartBtn);
        LoadBtn.onClick.AddListener(OnClickLoadBtn);
        SettingBtn.onClick.AddListener(OnClickSettingBtn);

        Process.GetInstance().SetProcess(ConstUtil.Process_Game_Start, Process.GetInstance().GetShowPlayerId());
    }

    // TODO: - 1 设置类似魔兽争霸的登录房间界面，新增扩展到 8 个，设置进入玩家，设置 AI 数量，全都设置进入列表，然后空的填 None
    public void InitPlayerEnterView()
    {

    }
    public void UpdatePlayerTypeList()
    {
        // TODO: - 1 操作设置一个就 set 一个
        Process.GetInstance().SetPlayerTypeList(ConstUtil.Player_Type_Human_Mine);
        for (int i = 1; i < ConstUtil.Max_Num_Player; i++)
        {
            Process.GetInstance().SetPlayerTypeList(ConstUtil.Player_Type_AI_Shit);
        }
    }
    private void OnClickStartBtn()
    {
        if (isLoadEnd)
        {
            // 新增玩家类型列表，由 PlayerSystem 进行玩家初始化，新增玩家 Id 列表
            UpdatePlayerTypeList();
            Process.GetInstance().SetProcess(ConstUtil.Process_Game_Start_Main_View, Process.GetInstance().GetShowPlayerId());
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnClickLoadBtn()
    {
        Debug.Log("MainView OnClickLoadBtn");
    }

    private void OnClickSettingBtn()
    {
        Debug.Log("MainView OnClickSettingBtn");
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: - 1 新增对应状态的进度条更新，更新完成后才能进入操作
        // isLoadEnd = true;
        // public const int Process_Game_Start_AI = 102;
        // public const int Process_Game_Start_Bartender = 103;
        // public const int Process_Game_Start_Heroes_Pool = 104;
        // public const int Process_Game_Start_Pieces_Pool = 105;
    }
}

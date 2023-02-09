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
    }

    // TODO: - 1 设置类似魔兽争霸的登录房间界面，新增扩展到 8 个，设置进入玩家，设置 AI 数量
    public void InitPlayerEnterView()
    {

    }
    public void UpdatePlayerTypeList()
    {
        Process.Instance.SetPlayerTypeList(ConstUtil.Player_Type_Human_Mine);
        for (int i = 1; i < ConstUtil.Max_Num_Player; i++)
        {
            Process.Instance.SetPlayerTypeList(ConstUtil.Player_Type_AI);
        }
    }
    private void OnClickStartBtn()
    {
        // 新增玩家类型列表，由 PlayerSystem 进行玩家初始化，新增玩家 Id 列表
        UpdatePlayerTypeList();
        Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Main_View, Process.Instance.GetShowPlayerId());
        GameObject.Destroy(this.gameObject);
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
        
    }
}

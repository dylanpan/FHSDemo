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

    private void OnClickStartBtn()
    {
        Process.Instance.SetProcess(ConstUtil.Process_Game_Start);
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

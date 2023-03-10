using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chess.Base;
using Chess.Component;
using Chess.Config;
using Chess.Util;

public class BartenderView : MonoBehaviour
{
    public Transform Bg;
    public TextMeshProUGUI Name;
    public Image Icon;
    public Button LevelBtn;
    public TextMeshProUGUI LevelBtnLabel;
    public Button CurrencyBtn;
    public TextMeshProUGUI CurrencyBtnLabel;
    public Button RefreshBtn;
    public TextMeshProUGUI RefreshBtnLabel;
    public Button FreezeBtn;
    public TextMeshProUGUI FreezeBtnLabel;
    public Transform PiecesLayout;
    void Awake()
    {
        EventUtil.Instance.Register(ConstUtil.Event_Type_update_bartender_pieces_view, UpdateBartenderPiecesView);
        EventUtil.Instance.Register(ConstUtil.Event_Type_update_bartender_currency, UpdateBartenderCurrency);
        EventUtil.Instance.Register(ConstUtil.Event_Type_update_bartender_level, UpdateBartenderLevel);
    }
    private void OnDestroy()
    {
        EventUtil.Instance.Unregister(ConstUtil.Event_Type_update_bartender_pieces_view, UpdateBartenderPiecesView);
        EventUtil.Instance.Unregister(ConstUtil.Event_Type_update_bartender_currency, UpdateBartenderCurrency);
        EventUtil.Instance.Unregister(ConstUtil.Event_Type_update_bartender_level, UpdateBartenderLevel);
    }
    // Start is called before the first frame update
    void Start()
    {
        InitView();
    }

    public void InitView()
    {
        Debug.Log("BartenderView InitView");
        CurrencyBtn.onClick.AddListener(OnClickCurrencyBtn);
        LevelBtn.onClick.AddListener(OnClickLevelBtn);
        RefreshBtn.onClick.AddListener(OnClickRefreshBtn);
        FreezeBtn.onClick.AddListener(OnClickFreezeBtn);

        FreezeBtnLabel.text = "LOCK";
        // TODO: 设置正确位置
        Bg.position += new Vector3(){x=0,y=500,z=0};
    }

    public void UpdateViewByData()
    {
        Entity player = World.Instance.entityDic[Process.GetInstance().GetShowPlayerId()];
        if (player != null)
        {
            PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
            if (playerComponent != null)
            {
                Entity bartender = World.Instance.entityDic[playerComponent.bartender_id];
                if (bartender != null)
                {
                    NameComponent nameComponent = (NameComponent)bartender.GetComponent<NameComponent>();
                    SkinComponent skinComponent = (SkinComponent)bartender.GetComponent<SkinComponent>();
                    LevelComponent levelComponent = (LevelComponent)bartender.GetComponent<LevelComponent>();
                    CurrencyComponent currencyComponent = (CurrencyComponent)bartender.GetComponent<CurrencyComponent>();
                    if (nameComponent != null)
                    {
                        Name.text = nameComponent.name;
                    }
                    if (skinComponent != null)
                    {
                        
                    }
                    if (levelComponent != null)
                    {
                        LevelBtnLabel.text = "Lv." + levelComponent.level.ToString();
                    }
                    if (currencyComponent != null)
                    {
                        RefreshBtnLabel.text = currencyComponent.refresh_cost.ToString();
                        CurrencyBtnLabel.text = currencyComponent.currency.ToString();
                    }
                    UpdateBartenderPiecesView(bartender.ID);
                }
            }
        }
    }
    private void UpdateBartenderCurrency(int currency)
    {
        CurrencyBtnLabel.text = currency.ToString();
    }
    private void UpdateBartenderLevel(int level)
    {
        LevelBtnLabel.text = "Lv." + level.ToString();
    }
    private void UpdateBartenderPiecesView(int id = ConstUtil.None)
    {
        if (id == ConstUtil.None)
        {
            Debug.Log("BartenderView UpdateBartenderPiecesView - no bartender");
        }
        else
        {
            Entity bartender = World.Instance.entityDic[id];
            if (bartender != null)
            {
                PiecesListComponent piecesListComponent = (PiecesListComponent)bartender.GetComponent<PiecesListComponent>();
                LevelComponent levelComponent = (LevelComponent)bartender.GetComponent<LevelComponent>();
                if (piecesListComponent != null && levelComponent != null)
                {
                    if (piecesListComponent.piecesIds.Count > 0)
                    {
                        CleanAllPiecesView();
                        for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                        {
                            Entity piece = World.Instance.entityDic[piecesListComponent.piecesIds[i]];
                            // TODO: 设置正确位置
                            AddPieceView(i, PiecesLayout, piecesListComponent.piecesIds[i], levelComponent.level, CommonUtil.Battle_GetEntityStatus(piece) == ConstUtil.Status_Piece_Freeze);
                        }
                        for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                        {
                            Debug.Log("BartenderView UpdateBartenderPiecesView - prepare: " + piecesListComponent.piecesIds[i].ToString());
                        }
                    }
                    else
                    {
                        Debug.Log("BartenderView UpdateBartenderPiecesView - prepare no");
                    }
                }
            }
        }
    }
    public void CleanAllPiecesView()
    {
        if (PiecesLayout.childCount > 0)
        {
            for (int i = 0; i < PiecesLayout.childCount; i++)
            {
                Transform child = PiecesLayout.GetChild(i);
                if (child != null)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }
    public void AddPieceView(int index, Transform parent, int id, int level, bool isFreeze)
    {
        GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/PieceView"));
        view.transform.parent = parent;
        Vector3 pos = GetPosByLevel(index, level);
        view.transform.position = pos;
        PieceView script = view.transform.GetComponent<PieceView>();
        script.UpdateViewByData(id, isFreeze);
    }

    private Vector3 GetPosByLevel(int index, int level)
    {
        Vector3 pos = new Vector3(){x=0,y=0,z=0};
        if (level < 2)
        {
            if (index == 0) { pos = new Vector3(){x=-240,y=200,z=0};}
            if (index == 1) { pos = new Vector3(){x=0,y=200,z=0};}
            if (index == 2) { pos = new Vector3(){x=240,y=200,z=0};}
        }
        else if (level < 5)
        {
            if (index == 0) { pos = new Vector3(){x=-360,y=200,z=0};}
            if (index == 1) { pos = new Vector3(){x=-120,y=200,z=0};}
            if (index == 2) { pos = new Vector3(){x=120,y=200,z=0};}
            if (index == 3) { pos = new Vector3(){x=360,y=200,z=0};}
        }
        else if (level < 6)
        {
            if (index == 0) { pos = new Vector3(){x=-480,y=200,z=0};}
            if (index == 1) { pos = new Vector3(){x=-240,y=200,z=0};}
            if (index == 2) { pos = new Vector3(){x=0,y=200,z=0};}
            if (index == 3) { pos = new Vector3(){x=240,y=200,z=0};}
            if (index == 4) { pos = new Vector3(){x=480,y=200,z=0};}
        }
        else
        {
            if (index == 0) { pos = new Vector3(){x=-600,y=200,z=0};}
            if (index == 1) { pos = new Vector3(){x=-360,y=200,z=0};}
            if (index == 2) { pos = new Vector3(){x=-120,y=200,z=0};}
            if (index == 3) { pos = new Vector3(){x=120,y=200,z=0};}
            if (index == 4) { pos = new Vector3(){x=360,y=200,z=0};}
            if (index == 5) { pos = new Vector3(){x=600,y=200,z=0};}
        }
        return pos;
    }

    public void OnClickCurrencyBtn()
    {
        Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Bartender_Refresh_Pre, Process.GetInstance().GetShowPlayerId());
    }
    public void OnClickLevelBtn()
    {
        Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Bartender_Level_Up, Process.GetInstance().GetShowPlayerId());
    }
    public void OnClickRefreshBtn()
    {
        Process.GetInstance().SetBartenderPieceFreezeState(false);
        Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Bartender_Refresh, Process.GetInstance().GetShowPlayerId());
    }
    public void OnClickFreezeBtn()
    {
        if (Process.GetInstance().GetBartenderPieceFreezeState())
        {
            Process.GetInstance().SetBartenderPieceFreezeState(false);
            Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Bartender_UnFreeze, Process.GetInstance().GetShowPlayerId());
        }
        else
        {
            Process.GetInstance().SetBartenderPieceFreezeState(true);
            Process.GetInstance().SetProcess(ConstUtil.Process_Prepare_Bartender_Freeze, Process.GetInstance().GetShowPlayerId());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

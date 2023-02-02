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
        Entity player = World.Instance.entityDic[Process.Instance.GetSelfPlayerId()];
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
                if (piecesListComponent != null)
                {
                    if (piecesListComponent.piecesIds.Count > 0)
                    {
                        CleanAllPiecesView();
                        for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                        {
                            Entity piece = World.Instance.entityDic[piecesListComponent.piecesIds[i]];
                            // TODO: 设置正确位置
                            AddPieceView(i, PiecesLayout, piecesListComponent.piecesIds[i], CommonUtil.Battle_GetEntityStatus(piece) == ConstUtil.Status_Piece_Freeze);
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
    public void AddPieceView(int index, Transform parent, int id, bool isFreeze)
    {
        GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/PieceView"));
        view.transform.parent = parent;
        Vector3 pos = new Vector3(){x=0,y=0,z=0};
        if (index == 0)
        {
            pos = new Vector3(){x=-240,y=200,z=0};
        }
        if (index == 1)
        {
            pos = new Vector3(){x=0,y=200,z=0};
        }
        if (index == 2)
        {
            pos = new Vector3(){x=240,y=200,z=0};
        }
        view.transform.position = pos;
        PieceView script = view.transform.GetComponent<PieceView>();
        script.UpdateViewByData(id, isFreeze);
    }

    public void OnClickCurrencyBtn()
    {
        Process.Instance.SetProcess(ConstUtil.Process_Battle_Start);
    }
    public void OnClickLevelBtn()
    {
        Process.Instance.SetProcess(ConstUtil.Process_Prepare_Bartender_Level_Up);
    }
    public void OnClickRefreshBtn()
    {
        Process.Instance.SetProcess(ConstUtil.Process_Prepare_Bartender_Refresh);
    }
    public void OnClickFreezeBtn()
    {
        // TODO: - 1 补充取消冻结
        Process.Instance.SetProcess(ConstUtil.Process_Prepare_Bartender_Freeze);
        // TODO: 补充购买棋子（手牌）状态逻辑 currencyComponent.currency >= currencyComponent.piece_cost
        // TODO: 补充出售棋子（手牌和战牌）状态逻辑 currencyComponent.currency += currencyComponent.piece_recycle
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

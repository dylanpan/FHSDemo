using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chess.Base;
using Chess.Component;
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
        EventUtil.Instance.Register(ConstUtil.Event_Type_update_bartender_pieces_view, updateBartenderPiecesView);
    }
    private void OnDestroy()
    {
        EventUtil.Instance.Unregister(ConstUtil.Event_Type_update_bartender_pieces_view, updateBartenderPiecesView);
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

    public void UpdateViewByData(int id = ConstUtil.None)
    {
        Entity bartender = null;
        if (id == ConstUtil.None)
        {
            bartender = World.Instance.entityDic[Process.Instance.GetBartenderPool()[0]];
            Process.Instance.SetBartenderId(bartender.ID);
        }
        else
        {
            bartender = World.Instance.entityDic[id];
        }
        NameComponent nameComponent = (NameComponent)bartender.GetComponent<NameComponent>();
        SkinComponent skinComponent = (SkinComponent)bartender.GetComponent<SkinComponent>();
        LevelComponent levelComponent = (LevelComponent)bartender.GetComponent<LevelComponent>();
        CurrencyComponent currencyComponent = (CurrencyComponent)bartender.GetComponent<CurrencyComponent>();
        if (nameComponent != null)
        {
            Name.text = nameComponent.name;
            TestUtil.SetBartender(nameComponent.id);
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
        updateBartenderPiecesView(bartender.ID);
    }
    private void updateBartenderPiecesView(int id = ConstUtil.None)
    {
        Entity bartender = null;
        if (id == ConstUtil.None)
        {
            Debug.Log("BartenderView updateBartenderPiecesView - no bartender");
        }
        else
        {
            bartender = World.Instance.entityDic[id];
        }
        PiecesListComponent piecesListComponent = (PiecesListComponent)bartender.GetComponent<PiecesListComponent>();
        if (piecesListComponent != null)
        {
            if (piecesListComponent.piecesIds.Count > 0)
            {
                for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                {
                    Entity piece = World.Instance.entityDic[piecesListComponent.piecesIds[i]];
                    // TODO: 设置正确位置
                    AddPieceView(PiecesLayout, new Vector3(){x=(i-1)*200,y=200,z=0}, piecesListComponent.piecesIds[i]);
                }
                for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                {
                    Debug.Log("BartenderView updateBartenderPiecesView - prepare: " + piecesListComponent.piecesIds[i].ToString());
                }
            }
            else
            {
                Debug.Log("BartenderView updateBartenderPiecesView - prepare no");
            }
        }
    }
    public void AddPieceView(Transform parent, Vector3 pos, int id)
    {
        GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/PieceView"));
        view.transform.parent = parent;
        view.transform.position += pos;
        PieceView script = view.transform.GetComponent<PieceView>();
        script.UpdateViewByData(id);
    }

    public void OnClickCurrencyBtn()
    {
        Process.Instance.SetProcess(ConstUtil.Process_Battle_Start);
    }
    public void OnClickLevelBtn()
    {
        // TODO: currencyComponent.currency >= currencyComponent.up_level_cost
    }
    public void OnClickRefreshBtn()
    {
        // TODO: currencyComponent.currency >= currencyComponent.refresh_cost
    }
    public void OnClickFreezeBtn()
    {
        // currencyComponent.currency >= currencyComponent.piece_cost
        // currencyComponent.currency += currencyComponent.piece_recycle
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

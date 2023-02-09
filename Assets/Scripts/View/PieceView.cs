using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chess.Base;
using Chess.Component;
using Chess.Util;

public class PieceView : MonoBehaviour
{
    public Button BgBtn;
    public Image Icon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Atk;
    public TextMeshProUGUI Hp;
    private Entity piece;
    private bool isDrag;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        InitView();
    }

    public void InitView()
    {
        Debug.Log("PieceView InitView");
    }

    public void UpdateViewByData(int id, bool isFreeze = false)
    {
        piece = World.Instance.entityDic[id];
        NameComponent nameComponent = (NameComponent)piece.GetComponent<NameComponent>();
        SkinComponent skinComponent = (SkinComponent)piece.GetComponent<SkinComponent>();
        PorpertyComponent porpertyComponent = (PorpertyComponent)piece.GetComponent<PorpertyComponent>();
        if (nameComponent != null)
        {
            Name.text = nameComponent.name;
        }
        if (skinComponent != null)
        {
            
        }
        if (porpertyComponent != null)
        {
            Atk.text = porpertyComponent.atk.ToString();
            Hp.text = porpertyComponent.hp.ToString();
        }
        BgBtn.transform.GetComponent<Image>().color = isFreeze ? new Color(0,100,180,255) : new Color(0,0,0,255);
    }
    // TODO: - 1 新增棋子拖拽效果，判断是否到达目标区域，执行对应操作，需要添加Collider
    private void OnMouseDown()
    {
        if (Input.touchCount == 1)
        {
            if (!isDrag)
            {
                isDrag = true;
                offset = this.transform.position - new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0);
            }
        }
    }
    private void OnMouseUp()
    {
        isDrag = false;
        if (CheckDragEndArea())
        {

        }
    }
    private void FixedUpdate()
    {
        if (Input.touchCount != 1)
        {
            isDrag = false;
        }
        if (isDrag)
        {
            this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0) + offset;
        }
    }
    private bool CheckDragEndArea()
    {
        bool isAim = false;
        return isAim;
    }
    private void OnPieceBuy()
    {
        if (piece != null)
        {
            // 从酒馆来
            if (CommonUtil.GetPieceBelong(piece.ID) == ConstUtil.Belong_Bartender)
            {
                TestUtil.SetBuyPieceId(piece.ID);
                Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Buy, Process.Instance.GetShowPlayerId());
            }
        }
    }
    private void OnPieceSell()
    {
        if (piece != null)
        {
            // 从战牌和手牌来
            if (CommonUtil.GetPieceBelong(piece.ID) == ConstUtil.Belong_Hand_Card || CommonUtil.GetPieceBelong(piece.ID) == ConstUtil.Belong_Battle_Card)
            {
                TestUtil.SetSellPieceId(piece.ID);
                Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Sell, Process.Instance.GetShowPlayerId());
            }
        }
    }
    private void OnPieceMove()
    {
        if (piece != null)
        {
            // 从战牌和手牌来
            if (CommonUtil.GetPieceBelong(piece.ID) == ConstUtil.Belong_Hand_Card || CommonUtil.GetPieceBelong(piece.ID) == ConstUtil.Belong_Battle_Card)
            {
                // 设置棋子状态
                CommonUtil.Battle_SetEntityStatus(piece, ConstUtil.Status_Piece_Move_B2B);
                TestUtil.SetMovePieceId(piece.ID);
                Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Move, Process.Instance.GetShowPlayerId());
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

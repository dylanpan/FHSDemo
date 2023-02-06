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
    // TODO: - 1 新增棋子拖拽效果，判断是否到达目标区域，执行对应操作：
    // 1.出售区域：执行出售逻辑；Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Sell); 设置 piece_sell_id
    // 2.1.手牌区域：执行购买（从酒馆来）逻辑；Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Buy); 设置 piece_buy_id
    // 2.2.手牌区域：执行移动（从战牌和手牌来）逻辑；Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Move); 设置棋子状态
    // 2.3.战牌区域：执行移动（从战牌和手牌来）逻辑

        // TODO: 补充购买棋子（手牌）状态逻辑 currencyComponent.currency >= currencyComponent.piece_cost
        // TODO: 补充出售棋子（手牌和战牌）状态逻辑 currencyComponent.currency += currencyComponent.piece_recycle
    private void OnPieceDragStart()
    {

    }
    private void CheckDragEndArea()
    {

    }
    private void OnPieceBuy()
    {
        // （从酒馆来）
        if (true && piece != null)
        {
            TestUtil.SetBuyPieceId(piece.ID);
            Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Buy);
        }
    }
    private void OnPieceSell()
    {
        // （从战牌和手牌来）
        if (true && piece != null)
        {
            TestUtil.SetSellPieceId(piece.ID);
            Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Sell);
        }
    }
    private void OnPieceMove()
    {
        // （从战牌和手牌来）
        if (true && piece != null)
        {
            // 设置棋子状态
            CommonUtil.Battle_SetEntityStatus(piece, ConstUtil.Status_Piece_Move_B2B);
            TestUtil.SetMovePieceId(piece.ID);
            Process.Instance.SetProcess(ConstUtil.Process_Prepare_Piece_Move);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

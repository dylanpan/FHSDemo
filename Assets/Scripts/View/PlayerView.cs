using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chess.Base;
using Chess.Component;
using Chess.Config;
using Chess.Util;

public class PlayerView : MonoBehaviour
{
    public Transform Bg;
    public Transform HeroesLayout;
    public Transform BattleCardLayout;
    public Transform HandCardLayout;
    void Awake()
    {
        EventUtil.Instance.Register(ConstUtil.Event_Type_update_bartender_currency, UpdateAllPiecesView);
    }
    private void OnDestroy()
    {
        EventUtil.Instance.Unregister(ConstUtil.Event_Type_update_bartender_currency, UpdateAllPiecesView);
    }
    // Start is called before the first frame update
    void Start()
    {
        InitView();
    }

    public void InitView()
    {
        Debug.Log("PlayerView InitView");

        Bg.position += new Vector3(){x=0,y=-500,z=0};
    }

    public void UpdateViewByData()
    {
        UpdateHeroView();
        UpdateBattleCardPiecesView();
        UpdateHandCardPiecesView();
    }
    private void UpdateHeroView()
    {
        Entity player = World.Instance.entityDic[Process.Instance.GetSelfPlayerId()];
        if (player != null)
        {
            PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
            if (playerComponent != null)
            {
                GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/HeroView"));
                view.transform.parent = HeroesLayout;
                HeroView script = view.transform.GetComponent<HeroView>();
                script.UpdateViewByData(playerComponent.hero_id);
            }
        }
    }
    private void UpdatePiecesView(Transform layout, int card_id, int radio)
    {
        Entity card = World.Instance.entityDic[card_id];
        PiecesListComponent piecesListComponent = (PiecesListComponent)card.GetComponent<PiecesListComponent>();
        if (piecesListComponent != null)
        {
            if (piecesListComponent.piecesIds.Count > 0)
            {
                CleanAllPiecesView(layout);
                for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                {
                    Entity piece = World.Instance.entityDic[piecesListComponent.piecesIds[i]];
                    // TODO: 设置正确位置
                    AddPieceView(piecesListComponent.piecesIds.Count, i, layout, piecesListComponent.piecesIds[i], radio);
                }
                for (int i = 0; i < piecesListComponent.piecesIds.Count; i++)
                {
                    Debug.Log("PlayerView UpdatePiecesView - prepare: " + piecesListComponent.piecesIds[i].ToString());
                }
            }
            else
            {
                Debug.Log("PlayerView UpdatePiecesView - prepare no");
            }
        }
    }
    public void UpdateHandCardPiecesView()
    {
        Entity player = World.Instance.entityDic[Process.Instance.GetSelfPlayerId()];
        if (player != null)
        {
            PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
            if (playerComponent != null)
            {
                UpdatePiecesView(HandCardLayout, playerComponent.hand_card_id, -750);
            }
        }
    }
    private void UpdateBattleCardPiecesView()
    {
        Entity player = World.Instance.entityDic[Process.Instance.GetSelfPlayerId()];
        if (player != null)
        {
            PlayerComponent playerComponent = (PlayerComponent)player.GetComponent<PlayerComponent>();
            if (playerComponent != null)
            {
                UpdatePiecesView(BattleCardLayout, playerComponent.battle_card_id, -250);
            }
        }
    }
    public void CleanAllPiecesView(Transform layout)
    {
        if (layout.childCount > 0)
        {
            for (int i = 0; i < layout.childCount; i++)
            {
                Transform child = layout.GetChild(i);
                if (child != null)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }
    public void AddPieceView(int total, int index, Transform parent, int id, int radio)
    {
        GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/PieceView"));
        view.transform.parent = parent;
        Vector3 pos = GetPosByLevel(total, index, radio);
        view.transform.position = pos;
        PieceView script = view.transform.GetComponent<PieceView>();
        script.UpdateViewByData(id);
    }

    private Vector3 GetPosByLevel(int total, int index, int radio)
    {
        Vector3 pos = new Vector3(){x=0,y=0,z=0};
        switch (total)
        {
            case 1:
            {
                if (index == 0) { pos = new Vector3(){x=0,y=radio,z=0};}
                break;
            }
            case 2:
            {
                if (index == 0) { pos = new Vector3(){x=-120,y=radio,z=0};}
                if (index == 1) { pos = new Vector3(){x=120,y=radio,z=0};}
                break;
            }
            case 3:
            {
                if (index == 0) { pos = new Vector3(){x=-240,y=radio,z=0};}
                if (index == 1) { pos = new Vector3(){x=0,y=radio,z=0};}
                if (index == 2) { pos = new Vector3(){x=240,y=radio,z=0};}
                break;
            }
            case 4:
            {
                if (index == 0) { pos = new Vector3(){x=-360,y=radio,z=0};}
                if (index == 1) { pos = new Vector3(){x=-120,y=radio,z=0};}
                if (index == 2) { pos = new Vector3(){x=120,y=radio,z=0};}
                if (index == 3) { pos = new Vector3(){x=360,y=radio,z=0};}
                break;
            }
            case 5:
            {
                if (index == 0) { pos = new Vector3(){x=-480,y=radio,z=0};}
                if (index == 1) { pos = new Vector3(){x=-240,y=radio,z=0};}
                if (index == 2) { pos = new Vector3(){x=0,y=radio,z=0};}
                if (index == 3) { pos = new Vector3(){x=240,y=radio,z=0};}
                if (index == 4) { pos = new Vector3(){x=480,y=radio,z=0};}
                break;
            }
            case 6:
            {
                if (index == 0) { pos = new Vector3(){x=-600,y=radio,z=0};}
                if (index == 1) { pos = new Vector3(){x=-360,y=radio,z=0};}
                if (index == 2) { pos = new Vector3(){x=-120,y=radio,z=0};}
                if (index == 3) { pos = new Vector3(){x=120,y=radio,z=0};}
                if (index == 4) { pos = new Vector3(){x=360,y=radio,z=0};}
                if (index == 5) { pos = new Vector3(){x=600,y=radio,z=0};}
                break;
            }
            case 7:
            {
                if (index == 0) { pos = new Vector3(){x=-720,y=radio,z=0};}
                if (index == 1) { pos = new Vector3(){x=-480,y=radio,z=0};}
                if (index == 2) { pos = new Vector3(){x=-240,y=radio,z=0};}
                if (index == 3) { pos = new Vector3(){x=0,y=radio,z=0};}
                if (index == 4) { pos = new Vector3(){x=240,y=radio,z=0};}
                if (index == 5) { pos = new Vector3(){x=480,y=radio,z=0};}
                if (index == 6) { pos = new Vector3(){x=720,y=radio,z=0};}
                break;
            }
            default:
                break;
        }
        return pos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

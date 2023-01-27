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

    public void UpdateViewByData(int id)
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

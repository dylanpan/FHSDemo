using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chess.Base;
using Chess.Component;
using Chess.Util;

public class HeroView : MonoBehaviour
{
    public Button BgBtn;
    public Image Icon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Atk;
    public TextMeshProUGUI Hp;
    private Entity hero;
    // Start is called before the first frame update
    void Start()
    {
        InitView();
    }

    public void InitView()
    {
        Debug.Log("HeroView InitView");
        BgBtn.onClick.AddListener(OnClickBgBtn);
    }

    public void UpdateViewByData(int id)
    {
        hero = World.Instance.entityDic[id];
        NameComponent nameComponent = (NameComponent)hero.GetComponent<NameComponent>();
        SkinComponent skinComponent = (SkinComponent)hero.GetComponent<SkinComponent>();
        PorpertyComponent porpertyComponent = (PorpertyComponent)hero.GetComponent<PorpertyComponent>();
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

    public void OnClickBgBtn()
    {
        TestUtil.SetHero(hero.ID);
        Process.Instance.SetProcess(ConstUtil.Process_Prepare_Start);
        EventUtil.Instance.SendEvent(ConstUtil.Event_Type_close_hero_pick_view);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

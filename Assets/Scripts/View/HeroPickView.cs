using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chess.Base;
using Chess.Util;

public class HeroPickView : MonoBehaviour
{
    public Transform Grid;
    // Start is called before the first frame update
    void Start()
    {
        InitView();
    }

    public void InitView()
    {
        Debug.Log("HeroPickView InitView");
        List<int> hero_pool = Process.Instance.GetHeroPoolFormDict(Process.Instance.GetSelfPlayerId());
        for (int i = 0; i < hero_pool.Count; i++)
        {
            AddHeroView(Grid, new Vector3(){x=(2*(i%2)-1)*200,y=0,z=0}, hero_pool[i]);
        }
    }
    public void AddHeroView(Transform parent, Vector3 pos, int id)
    {
        GameObject view = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/HeroView"));
        view.transform.parent = parent;
        view.transform.position += pos;
        HeroView script = view.transform.GetComponent<HeroView>();
        script.UpdateViewByData(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

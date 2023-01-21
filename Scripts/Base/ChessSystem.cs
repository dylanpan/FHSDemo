using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MHU3D;

public class ChessSystem : MonoBehaviour
{
    private LuaManager luaMgr;
    private float startTime = 0f;
    private float endTime = 0f;
    private int step = 0;
    private GameObject arrowObj;
    void Start()
    {
        Debug.Log("Loading ChessSystem");

        DontDestroyOnLoad(this.gameObject);
        luaMgr = transform.gameObject.AddComponent<ChessSystemLua>();


        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Chess/ChessCube.prefab");
        this.AddAllCube("Player", prefab);
        this.AddAllCube("NonPlayer", prefab);

        Transform transOperation = transform.Find("SpawnPosition/Operation");
        this.AddCube(transOperation, prefab);

        GameObject arrowPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Chess/ChessArrow.prefab");
        arrowObj = this.AddCube(transform.Find("SpawnPosition/"), arrowPrefab);
        ChessArrow chessArrowCom = arrowObj.GetComponent<ChessArrow>();
        chessArrowCom.inputStartPos = new Vector3(-3, 0, 0);
        chessArrowCom.inputEndPos = new Vector3(3, 0, 0);
    }

    private void AddAllCube(string tag, GameObject prefab)
    {
        Transform transPiecesArea = transform.Find("SpawnPosition/" + tag + "/PiecesArea");
        foreach(Transform transPieces in transPiecesArea.GetComponentsInChildren<Transform>())
        {
            if (transPieces.name != "PiecesArea")
            {
                GameObject piecesCube = this.AddCube(transPieces, prefab);
                piecesCube.name = tag + "_" + transPieces.name;
            }
        }

        Transform transHeroArea = transform.Find("SpawnPosition/" + tag + "/HeroArea");
        GameObject heroCube = this.AddCube(transHeroArea, prefab);
        heroCube.name = tag + "_HeroArea";
    }
    private GameObject AddCube(Transform parent, GameObject prefab)
    {
        GameObject _instObj = Instantiate(prefab);
        _instObj.transform.SetParent(parent);
        _instObj.transform.localPosition = new Vector3(0,0,0);
        return _instObj;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void FixedUpdate()
    {
        // testChess 动态修改箭头指向
        startTime = Time.time;
        if (startTime - endTime == 20.0f)
        {
            endTime = Time.time;
            step++;

            ChessArrow chessArrowCom = arrowObj.GetComponent<ChessArrow>();
            chessArrowCom.inputStartPos = new Vector3(-step, 0, 0);
            chessArrowCom.inputEndPos = new Vector3(step, 0, 0);
        }
    }
}

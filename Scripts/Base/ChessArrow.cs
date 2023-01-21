using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MHU3D;

public class ChessArrow : MonoBehaviour
{
    private Transform transCube;
    public Vector3 inputStartPos;
    public Vector3 inputEndPos;
    public Vector3 lastInputStartPos;
    public Vector3 lastInputEndPos;
    private ArrayList gameObjectArrowList;
    private ArrayList gameObjectPosList;
    private Vector3[] updatePath;
    private float startTime = 0f;
    private float endTime = 0f;
    private int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObjectArrowList = new ArrayList();
        gameObjectPosList = new ArrayList();
        transCube = transform.Find("Cube");
    }

    void OnDrawGizmos()
    {
        Vector3 _startPos = new Vector3(-3, 0, 0);
        Vector3 _endPos = new Vector3(3, 0, 0);

        Vector3 dir = _endPos - _startPos;
        Vector3 otherDir = Vector3.up;
        Vector3 planeNormal = Vector3.Cross(otherDir, dir);
        Vector3 vertical = Vector3.Cross(dir, planeNormal).normalized;
        Vector3 centerPos = (_startPos + _endPos) * 0.5f;
        // 控制点
        Vector3 controlPos = GetControlPos(_startPos, _endPos, 5f);

        // 线段
        Gizmos.color = Color.white;
        Gizmos.DrawLine(_startPos, _endPos);

        // 平面法线
        Gizmos.color = Color.red;
        Gizmos.DrawLine(centerPos, controlPos + planeNormal.normalized * 2);

        // 设置方向
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_startPos, _startPos + otherDir.normalized * 2);
        // 设置方向
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_endPos, _endPos + otherDir.normalized * 2);

        // 中垂线
        Gizmos.color = Color.green;
        Gizmos.DrawLine(centerPos, centerPos + vertical.normalized * 5);

        // 曲线
        Vector3[] path = GetCurvePath(10, _startPos, _endPos);
        Vector3 firstPos = path[0];
        for (int i = 1; i < path.Length; i++)
        {
            Vector3 nextPos = path[i];
            Gizmos.color = Color.yellow;
            Vector3 pos = (firstPos + nextPos) * 0.5f;
			Gizmos.DrawSphere(pos, 0.25f);
            firstPos = nextPos;
        }
    }

    private Vector3 GetControlPos(Vector3 startPos, Vector3 endPos, float offset)
    {
        // 方向
        Vector3 dir = endPos - startPos;
        // 另一个方向
        Vector3 otherDir = Vector3.up;
        // 平面法线
        Vector3 planeNormal = Vector3.Cross(otherDir, dir);
        // startPos 和 endPos 的垂线
        Vector3 vertical = Vector3.Cross(dir, planeNormal).normalized;
        // 中点
        Vector3 centerPos = (startPos + endPos) * 0.5f;
        // 控制点
        Vector3 controlPos = centerPos + vertical * offset;

        return controlPos;
    }

    private Vector3[] GetCurvePath(int sampleNum, Vector3 startPos, Vector3 endPos)
    {
        Vector3 controlPos = GetControlPos(startPos, endPos, 5f);
        Vector3[] path = new Vector3[sampleNum + 1];
        path[0] = startPos;
        for (int i = 1; i <= sampleNum; i++)
        {
            float t = i / (float) sampleNum;
            float x = (1 - t) * (1 - t) * startPos.x + 2 * t * (1 - t) * controlPos.x + t * t * endPos.x;
            float y = (1 - t) * (1 - t) * startPos.y + 2 * t * (1 - t) * controlPos.y + t * t * endPos.y;
            float z = (1 - t) * (1 - t) * startPos.z + 2 * t * (1 - t) * controlPos.z + t * t * endPos.z;
            Vector3 point = new Vector3(x, y, z);
            path[i] = point;
        }
        return path;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // 曲线 
        // todoChess 曲线展示形式优化
        if (lastInputStartPos != inputStartPos)
        {
            lastInputStartPos = inputStartPos;
            updatePath = null;
        }
        if (lastInputEndPos != inputEndPos)
        {
            lastInputEndPos = inputEndPos;
            updatePath = null;
        }
        if (updatePath == null)
        {
            gameObjectPosList.Clear();
            updatePath = GetCurvePath(20, lastInputStartPos, lastInputEndPos);
        }
        if (updatePath != null && updatePath.Length > 0 && gameObjectPosList.Count <= 0)
        {
            Vector3 firstPos = updatePath[0];
            for (int i = 1; i < updatePath.Length; i++)
            {
                Vector3 nextPos = updatePath[i];
                Vector3 pos = (firstPos + nextPos) * 0.5f;
                gameObjectPosList.Add(pos);
                firstPos = nextPos;
            }
        }
        if (gameObjectArrowList.Count <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                // 新增 3 个方块
                GameObject _instObj = Instantiate(transCube.gameObject);
                _instObj.transform.parent = transform;
                gameObjectArrowList.Add(_instObj);
            }
        }

        // 3 个方块，依次移动
        startTime = Time.time;
        if (startTime - endTime == 1.0f)
        {
            endTime = Time.time;
            step++;
            if (gameObjectPosList.Count > 0)
            {
                int index_0 = step;
                int index_1 = step + 1;
                int index_2 = step + 2;
                if (index_1 >= gameObjectPosList.Count)
                {
                    index_1 = index_1 - gameObjectPosList.Count;
                }
                if (index_2 >= gameObjectPosList.Count)
                {
                    index_2 = index_2 - gameObjectPosList.Count;
                    step = 0;
                }
                ((GameObject)gameObjectArrowList[0]).transform.position = (Vector3)gameObjectPosList[index_0];
                ((GameObject)gameObjectArrowList[1]).transform.position = (Vector3)gameObjectPosList[index_1];
                ((GameObject)gameObjectArrowList[2]).transform.position = (Vector3)gameObjectPosList[index_2];
            }
        }
    }
}

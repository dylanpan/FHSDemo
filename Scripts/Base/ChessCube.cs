using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MHU3D;
using DG.Tweening;

public class ChessCube : MonoBehaviour
{
    private Transform _transform;
    private bool isDrag;
    private bool isMoving;
    private bool isStay;
    private Vector3 oriPos;
    private Vector3 oriCubeScreenPos;
    private Vector3 oriMousePos;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        oriPos = _transform.position;
    }

    void OnTriggerEnter(Collider other)
    {   
        if (isDrag)
        {
            Debug.Log(gameObject.name + " be hit by " + other.name);
            string[] sourceNameList = gameObject.name.Split('_');
            string[] otherNameList = other.name.Split('_');
            if (!isMoving && sourceNameList[0] == otherNameList[0] && otherNameList[1] != "HeroArea")
            {
                int sourceNum = 0;
                int otherNum = 0;
                int.TryParse(sourceNameList[1], out sourceNum);
                int.TryParse(otherNameList[1], out otherNum);
                Debug.Log("OnTriggerEnter : " + sourceNum + " " + otherNum);
                if (sourceNum != otherNum)
                {
                    bool isRight = sourceNameList[0] == "Player" ? (sourceNum < otherNum) : (sourceNum > otherNum);
                    isMoving = true;
                    // 需要移动
                    Sequence seq = DOTween.Sequence();
                    // 移动到对方碰撞体的原有位置
                    seq.Append(other.transform.DOLocalMoveZ(0.7f * (isRight ? 1 : -1), 0.2f));
                    // 移动结束后交换父节点
                    seq.AppendCallback(() => {
                        Transform _tmpParent = other.gameObject.transform.parent;
                        other.transform.parent = transform.parent;
                        transform.parent = _tmpParent;

                        string _tmpName = other.name;
                        other.name = gameObject.name;
                        gameObject.name = _tmpName;

                        isMoving = false;

                        transform.localPosition = new Vector3(0,0,0);
                        other.gameObject.transform.localPosition = new Vector3(0,0,0);
                    });
                }
            }

            if (sourceNameList[0] == "Player" && other.name == "NonPlayer_HeroArea")
            {
                Debug.Log("OnTriggerEnter: " + gameObject.name);
                isStay = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isDrag)
        {
            string[] sourceNameList = gameObject.name.Split('_');
            if (sourceNameList[0] == "Player" && other.name == "NonPlayer_HeroArea")
            {
                Debug.Log("OnTriggerExit: " + gameObject.name);
                isStay = false;
            }
        }
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            oriCubeScreenPos = Camera.main.WorldToScreenPoint(_transform.position);
            oriMousePos = Input.mousePosition;
        }
    }

    void OnMouseUp()
    {
        isDrag = false;
        _transform.localPosition = new Vector3(0,0,0);
        if (isStay)
        {
            Debug.Log("DestroyImmediate: " + gameObject.name);
            DestroyImmediate(gameObject);
        }
    }

    void OnMouseDrag()
    {
        string[] sourceNameList = gameObject.name.Split('_');
        if (sourceNameList[1] != "HeroArea")
        {
            isDrag = true;
            Vector3 curMousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(curMousePos);
            float c = Mathf.Cos(Vector3.Angle(Vector3.down, ray.direction.normalized) * Mathf.PI / 180);
            float height = Camera.main.transform.position.y - oriPos.y - 0.2f;
            float percent = height / c;
            Vector3 result = Camera.main.transform.position + ray.direction.normalized * percent;
            _transform.position = new Vector3(result.x, _transform.position.y, result.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

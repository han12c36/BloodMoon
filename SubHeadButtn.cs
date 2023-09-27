using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SubHeadButtn : MonoBehaviour
{
    //해당 버튼을 누르게 되면 자식을 가져온다. 
    //해당 자식들이 현재 해당 헤드 위치에서 동시에 각자의 위치로 이동을 한다. -> 자연스러운 이동

    //이렇게 받아도 됨
    //근데 이건 실수한다이가.

    public GameObject childGroud;
    RectTransform[] childRectTransforms;
    int count;
    RectTransform rectTr;
    Vector3 startPos;
    Vector3[] targetPos;

    bool isShow;

    public void Start() 
    {
        rectTr = GetComponent<RectTransform>();
        startPos = rectTr.anchoredPosition;

        count = childGroud.transform.childCount;
        childRectTransforms = new RectTransform[count];

        targetPos = new Vector3[count];

        LayoutRebuilder.ForceRebuildLayoutImmediate(childGroud.GetComponent<RectTransform>());

        for (int i = 0; i < count; i++) 
        {
            childRectTransforms[i] = childGroud.transform.GetChild(i).GetComponent<RectTransform>();

            //targetPos[i] = childRectTransforms[i].anchoredPosition;

            //Debug.Log(targetPos[i].y);
        }

        targetPos[0] = new Vector3(60, -60, 0);
        targetPos[1] = new Vector3(60, -230, 0);
        targetPos[2] = new Vector3(60, -400, 0);
        targetPos[3] = new Vector3(60, -570, 0);
        targetPos[4] = new Vector3(60, -740, 0);

        isShow = false;
        childGroud.gameObject.SetActive(isShow);
    }


    void ShowChild()
    {
        childGroud.gameObject.SetActive(isShow = true);

        for (int i = 0; i < count; i++)
        {
            childRectTransforms[i].localPosition = new Vector3(60, 180, 0);
        }


        for (int i = 0; i < count; i++)
        {
            //현재 위치에서 각자 위치로 출발
            childRectTransforms[i].DOAnchorPosY(targetPos[i].y,0.5f);
        }
    }

    void HideChild()
    {
        for (int i = 0; i < count; i++)
        {
            //현재 위치에서 각자 위치로 출발
            childRectTransforms[i].localPosition = targetPos[i];

            childRectTransforms[i].DOAnchorPosY(startPos.y, 0.5f).OnComplete(() => 
            {
                childGroud.gameObject.SetActive(isShow = false);
            });
        }
    }

    public void TryOpenChild()
    {
        ShowChild();

        //if (isShow) HideChild();
        //else ShowChild();
    }
}

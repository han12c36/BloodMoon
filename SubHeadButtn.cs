using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SubHeadButtn : MonoBehaviour
{
    //�ش� ��ư�� ������ �Ǹ� �ڽ��� �����´�. 
    //�ش� �ڽĵ��� ���� �ش� ��� ��ġ���� ���ÿ� ������ ��ġ�� �̵��� �Ѵ�. -> �ڿ������� �̵�

    //�̷��� �޾Ƶ� ��
    //�ٵ� �̰� �Ǽ��Ѵ��̰�.

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
            //���� ��ġ���� ���� ��ġ�� ���
            childRectTransforms[i].DOAnchorPosY(targetPos[i].y,0.5f);
        }
    }

    void HideChild()
    {
        for (int i = 0; i < count; i++)
        {
            //���� ��ġ���� ���� ��ġ�� ���
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

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class RevertButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public Image fillImage;
    public float amountPerDelta = 1.0f;

    public bool isComplete => fillImage.fillAmount > 0.99f;

    public bool isFillStart = false;

    public void FillRevert()
    {
        if(fillImage.fillAmount < 1.0f) fillImage.fillAmount += (amountPerDelta * Time.deltaTime) / 1.0f;
    }

    // 드래그 시작
    // 하위 오브젝트를 활성화 하는 방식으로 갑시다. 업데이트는 하위 오브젝트가 진행을 합니다.
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        isFillStart = true;

        Debug.Log("!");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        isFillStart = false;

        if (isComplete) fillImage.fillAmount = 1.0f;
        else fillImage.fillAmount = .0f;
    }

    public void Update()
    {
        if(isFillStart)
        {
            FillRevert();
        }
    }
}
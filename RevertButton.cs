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

    // �巡�� ����
    // ���� ������Ʈ�� Ȱ��ȭ �ϴ� ������� ���ô�. ������Ʈ�� ���� ������Ʈ�� ������ �մϴ�.
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
using UnityEngine;
using UnityEngine.EventSystems;

public enum GrowthContents
{
    Stat,
    Skill,
    Item,
    Summoning,
    Gacha,
}


public class DragButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform target;
    public Window window;
    
    public static Vector2 DefaultPos { get; private set; }
    private Vector2 offset;
    private RectTransform rect;
    private float radius;
    public void Awake()
    {
        rect = GetComponent<RectTransform>();
        radius = rect.rect.width * 0.5f;
    }

    // �巡�� ���� 
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        offset = (Vector2)transform.position - eventData.position;
        DefaultPos = transform.position;
    }

    // �巡�� ��
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;

        transform.position = currentPos + offset;
    }
    
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (IsPointInsideCircle(transform.position, target)) 
        {
            //viewModel.model = GetModel();

            window.OpenWindow();
        }

        this.transform.position = DefaultPos;
    }

    bool IsPointInsideCircle(Vector2 point, RectTransform circle)
    {
        Vector2 circleCenter = circle.position;
        float circleRadius = circle.rect.width * 0.5f; // �������� ���� ���� ������ ����

        // ���� �� �߽� ������ �Ÿ� ���
        float distance = Vector2.Distance(point, circleCenter);

        // ���� �� �߽� ������ �Ÿ��� ���������� �۰ų� ������ �� ���ο� ����

        return distance < circleRadius + radius;
    }

    public void Test()
    {
        Debug.Log("�巡�װ� Ÿ�� ���� �������ϴ�.");
    }
}
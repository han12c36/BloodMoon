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

    // 드래그 시작 
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        offset = (Vector2)transform.position - eventData.position;
        DefaultPos = transform.position;
    }

    // 드래그 중
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
        float circleRadius = circle.rect.width * 0.5f; // 반지름은 원의 가로 길이의 절반

        // 점과 원 중심 사이의 거리 계산
        float distance = Vector2.Distance(point, circleCenter);

        // 점과 원 중심 사이의 거리가 반지름보다 작거나 같으면 원 내부에 있음

        return distance < circleRadius + radius;
    }

    public void Test()
    {
        Debug.Log("드래그가 타겟 위에 놓였습니다.");
    }
}
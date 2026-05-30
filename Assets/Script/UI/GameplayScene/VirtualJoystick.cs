using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("References")]
    public RectTransform joystickOuterRing;
    public RectTransform handle;

    [Header("Mode")]
    [SerializeField]
    [Tooltip("On/Off floating joystick")]
    private bool floating = false;
    public bool Floating => floating;

    private RectTransform panelRect;

    private bool activeTouch = false;

    private Vector2 input;
    public Vector2 Input => input;

    private float touchRadius;
    private float dragRadius;

    [Tooltip("Offset from anchored position of outer ring joystick")]
    public Vector2 defaultOuterRingPos = new Vector2(0,300);

    private void Awake()
    {
        panelRect = transform as RectTransform;

        RecalculateRadius();
    }

    private void Start()
    {
        ApplyMode();
    }

    public void SetFloating(bool value)
    {
        if (floating == value)
            return;

        floating = value;
        ApplyMode();
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            RecalculateRadius();
            ApplyMode();
        }
    }

    private void ApplyMode()
    {
        activeTouch = false;
        input = Vector2.zero;

        joystickOuterRing.anchoredPosition = defaultOuterRingPos;
        handle.anchoredPosition = Vector2.zero;

        joystickOuterRing.gameObject.SetActive(!floating);
    }

    private void RecalculateRadius()
    {
        float outer = joystickOuterRing.rect.width * 0.5f;
        float inner = handle.rect.width * 0.5f;

        touchRadius = outer;
        dragRadius = outer - inner;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (activeTouch)
            return;

        //vị trí chạm trong panel local position
        Vector2 panelPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelRect,
            eventData.position,
            eventData.pressEventCamera,
            out panelPoint
        );

        //Debug.Log($"panel x = {panelPoint.x}, panel y = {panelPoint.y}");

        // vị trí joystick trên panel local position
        Vector2 ringCenter;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelRect,
            RectTransformUtility.WorldToScreenPoint(
                eventData.pressEventCamera,
                joystickOuterRing.position
            ),
            eventData.pressEventCamera,
            out ringCenter
        );

        if (floating)
        {
            joystickOuterRing.gameObject.SetActive(true);
            joystickOuterRing.position = eventData.position;
        }
        else
        {
            Vector2 delta = panelPoint - ringCenter;

            // chỉ cần chạm trong outerRing
            if (delta.magnitude > touchRadius)
                return;
        }
        activeTouch = true;

        handle.anchoredPosition = Vector2.zero;
        input = Vector2.zero;

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!activeTouch)
            return;

        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickOuterRing,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        Vector2 delta = Vector2.ClampMagnitude(localPoint, dragRadius);

        handle.anchoredPosition = delta;
        input = delta / dragRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        activeTouch = false;

        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

        if (floating)
            joystickOuterRing.gameObject.SetActive(false);
    }
}
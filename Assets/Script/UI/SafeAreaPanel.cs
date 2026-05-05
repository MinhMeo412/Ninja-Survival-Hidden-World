using UnityEngine;

/// <summary>
/// Lớp hỗ trợ tự động điều chỉnh RectTransform để phù hợp với Safe Area trên các thiết bị di động.
/// 
/// Safe Area là vùng màn hình an toàn, không bị che khuất bởi notch, dynamic island, 
/// thanh trạng thái, thanh điều hướng... trên iPhone, Android cao cấp.
/// 
/// Script này nên được gắn vào một UI Panel/Canvas con có anchor bao phủ toàn màn hình.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    private Rect lastSafeArea;

    private void Awake()
    {
        ApplySafeArea();
    }

    private void Update()
    {
        if (Screen.safeArea != lastSafeArea)
        {
            ApplySafeArea();
        }
    }

    private void ApplySafeArea()
    {
        Rect safe = Screen.safeArea;
        lastSafeArea = safe;

        RectTransform rectTransform = GetComponent<RectTransform>();

        // Tính toán anchorMin và anchorMax theo tỉ lệ màn hình
        Vector2 anchorMin = safe.position;
        Vector2 anchorMax = safe.position + safe.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        // Reset offset để đảm bảo không bị lệch
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}

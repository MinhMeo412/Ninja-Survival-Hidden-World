using UnityEngine;

public class MobileUIAutoConfig : MonoBehaviour
{
    void Awake()
    {
#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_EDITOR
            gameObject.SetActive(false); 
#endif
    }
}
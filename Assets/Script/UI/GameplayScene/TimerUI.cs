using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Awake()
    {
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameplayTimer.Instance.OnTimeChanged += UpdateUI;

        UpdateUI(GameplayTimer.Instance.currentTime);
    }

    private void OnDestroy()
    {
        if (GameplayTimer.Instance != null)
        {
            GameplayTimer.Instance.OnTimeChanged -= UpdateUI;
        }
    }

    private void UpdateUI(float time)
    {
        timerText.text = GameplayTimer.Instance.FormattedTime;
    }    
}

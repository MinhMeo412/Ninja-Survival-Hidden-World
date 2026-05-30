using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText;

    private void OnEnable()
    {
        PlayerLevelManager.OnExpChanged += UpdateText;
    }

    private void UpdateText(PlayerLevelManager stats)
    {
        levelText.text = $"Level {stats.level}: {stats.currentExp}/{stats.maxExp}";
    }
}

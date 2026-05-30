using TMPro;
using UnityEngine;

public class HealthPointUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI hpText;

    private void OnEnable()
    {
        CharacterStats.OnPlayerHealthChanged += UpdateText;
    }

    private void UpdateText(CharacterStats stats)
    {
        hpText.text = $"HP: {stats.currentHP}/{stats.maxHP}";
    }
}

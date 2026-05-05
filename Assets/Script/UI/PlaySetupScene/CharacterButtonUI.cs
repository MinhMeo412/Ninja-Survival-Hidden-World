using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private Image border;
    [SerializeField] private Button button;

    private CharacterProfile profile;

    public Action<CharacterButtonUI> OnCharacterSelected;

    public void Setup(CharacterProfile data)
    {
        profile = data;

        icon.sprite = profile.icon;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        SetSelected(false);
    }

    private void OnClick()
    {
        OnCharacterSelected?.Invoke(this);
    }

    public void SetSelected(bool selected)
    {
        border.enabled = selected;
    }

    public CharacterProfile GetProfile()
    {
        return profile;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent;
    public GameObject characterButtonPrefab; 

    public Button nextButton;

    [Header("Data")]
    public EntityList entityList;

    private List<CharacterButtonUI> allButtons = new List<CharacterButtonUI>();

    private void Start()
    {
        if (nextButton != null) nextButton.interactable = false;

        GenerateButtons();
    }

    private void GenerateButtons()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        allButtons.Clear();

        if (entityList is CharacterList characterList)
        {
            foreach (CharacterProfile profile in characterList.profiles)
            {
                GameObject newButton = Instantiate(characterButtonPrefab, contentParent);

                CharacterButtonUI ui = newButton.GetComponent<CharacterButtonUI>();
                ui.Setup(profile);
                ui.OnCharacterSelected = HandleSelection;

                allButtons.Add(ui);
            }
        }

        if (allButtons.Count > 0) HandleSelection(allButtons[0]);
    }

    private void HandleSelection(CharacterButtonUI selectedButton)
    {
        foreach (var btn in allButtons)
        {
            // Nếu là nút vừa bấm thì bật border, ngược lại tắt hết
            btn.SetSelected(btn == selectedButton);
        }

        GameSession.Instance.selectedCharacter = selectedButton.GetProfile();

        if (nextButton != null) nextButton.interactable = true;
    }
}

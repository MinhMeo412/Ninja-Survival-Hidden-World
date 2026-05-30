using UnityEngine;

public class UIButtonAction : MonoBehaviour
{
    public ButtonActionType actionType;

    [Header("Scene Settings")]
    public string sceneName;
    public SceneTransitionType transitionType;

    [Header("Panel Settings")]
    public GameObject panel;
    public PanelActionToggle actionToggle;

    public void Execute()
    {
        switch (actionType)
        {
            case ButtonActionType.LoadScene:
                SceneLoader.Instance.Load(sceneName, transitionType);
                break;

            case ButtonActionType.BackToMenu:
                SceneLoader.Instance.Load("SC_MainMenu", SceneTransitionType.Faded);
                if(Time.timeScale == 0)
                {
                    Debug.Log("Đổi time scale");
                    Time.timeScale = 1f;
                }    
                break;

            case ButtonActionType.Panel:
                if (actionToggle == PanelActionToggle.Open)
                { panel.SetActive(true); }
                else { panel.SetActive(false); }
                break;

            case ButtonActionType.QuitGame:
                ApplicationController.Instance.QuitGame();
                break;
        }
    }
}
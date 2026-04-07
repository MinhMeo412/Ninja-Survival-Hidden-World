using UnityEngine;

public class UIButtonAction : MonoBehaviour
{
    public ButtonActionType actionType;

    [Header("Scene Settings")]
    public string sceneName;
    public SceneTransitionType transitionType;

    public void Execute()
    {
        switch (actionType)
        {
            case ButtonActionType.LoadScene:
                SceneLoader.Instance.Load(sceneName, transitionType);
                break;

            case ButtonActionType.BackToMenu:
                SceneLoader.Instance.Load("SC_MainMenu", SceneTransitionType.Faded);
                break;

            case ButtonActionType.QuitGame:
                ApplicationController.Instance.QuitGame();
                break;
        }
    }
}
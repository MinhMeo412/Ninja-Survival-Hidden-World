using UnityEngine;

public class GameOverPanelController : MonoBehaviour
{
    void Awake()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.OnGameOver += ShowGameOverPanel;
    }

    void ShowGameOverPanel()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }    
}

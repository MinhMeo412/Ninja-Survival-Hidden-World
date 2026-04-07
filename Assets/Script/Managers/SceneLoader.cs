using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Load(string sceneName, SceneTransitionType type)
    {
        switch (type)
        {
            case SceneTransitionType.Normal:
                StartFade();
                break;

            case SceneTransitionType.Reload:
                GameManager.Instance.BeforeSceneChange();
                break;


            case SceneTransitionType.Instant:
                break;
        }

        SceneManager.LoadScene(sceneName);
    }

    void StartFade()
    {
        Debug.Log("Fade animation");
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}

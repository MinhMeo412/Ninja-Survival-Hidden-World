using System;
using Unity.Jobs;
using UnityEngine;

public class GameplayTimer : MonoBehaviour 
{
    public static GameplayTimer Instance { get; private set; }

    public event Action<float> OnTimeChanged;
    public event Action OnTimerPaused;
    public event Action OnTimerResumed;

    public float currentTime { get; private set; }
    private int lastSecond;

    public bool isRunning; //{ get; private set; }

    public string FormattedTime
    {
        get
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            return $"{minutes:00}:{seconds:00}";
        }
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (!isRunning)
            return;

        currentTime += Time.deltaTime;

        int currentSecond = Mathf.FloorToInt(currentTime);

        if (currentSecond != lastSecond)
        {
            lastSecond = currentSecond;

            OnTimeChanged?.Invoke(currentTime);
        }
    }

    public void StartTimer()
    {
        currentTime = 0f;

        isRunning = true;
    }    

    public void PauseTimer()
    {
        if (!isRunning) return;

        isRunning = false;

        OnTimerPaused?.Invoke();
    }    

    public void ResumeTimer()
    {
        if (isRunning)
            return;

        isRunning = true;

        OnTimerResumed?.Invoke();
    }    

    public void ResetTimer()
    {
        currentTime = 0f;

        OnTimeChanged?.Invoke(currentTime);
    }    
}

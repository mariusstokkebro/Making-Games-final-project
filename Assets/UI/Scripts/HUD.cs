using System;
using System.Timers;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floorDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    
    // TODO Should be moved to a game manager class
    private TimeSpan timerValue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerValue = TimeSpan.Zero;
        // TODO remove
        UpdateFloorDisplay(1);
    }

    // Update is called once per frame
    void Update()
    {
        timerValue += TimeSpan.FromSeconds(Time.deltaTime);
        UpdateTimerDisplay(timerValue);
    }

    public void UpdateFloorDisplay(int floor)
    {
        floorDisplay.text = "Floor " + floor;
    }

    public void UpdateTimerDisplay(TimeSpan time)
    {
        timerDisplay.text = time.ToString("mm':'ss");
    }
    
}

using System;
using System.Timers;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    // Singleton instance
    public static HUD Instance;
    
    [SerializeField] private TextMeshProUGUI floorDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    [SerializeField] private HealthBar healthBarDisplay;
    
    // TODO Should be moved to a game manager class
    private TimeSpan timerValue;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerValue = TimeSpan.Zero;
        
        // Initialization, TODO remove
        UpdateFloorDisplay(1);
        healthBarDisplay.Initialize(6, 1);
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

    public void InitializeHealthBar(float maxHP, float HPperIcon)
    {
        healthBarDisplay.Initialize(maxHP, HPperIcon);
    }
    
    public void UpdateHealthBar(float newHP)
    {
        healthBarDisplay.UpdateHealthBar(newHP);
    }
    
    // TODO item display
    
}

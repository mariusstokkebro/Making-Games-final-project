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
    [SerializeField] private WeaponSlot primaryWeaponSlot;
    [SerializeField] private WeaponSlot secondaryWeaponSlot;

    private bool usingPrimaryWeapon = true;
    
    // TODO Should probably be moved to a game manager class
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
        //healthBarDisplay.Initialize(6, 1);
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

    public void ReduceWeaponUses()
    {
        if(usingPrimaryWeapon)
            primaryWeaponSlot.ReduceUses();
        else
            secondaryWeaponSlot.ReduceUses();
    }

    public void SetPrimaryWeapon(Weapon weapon)
    {
        primaryWeaponSlot.FillSlot(weapon);
    }
    
    public void SetSecondaryWeapon(Weapon weapon)
    {
        secondaryWeaponSlot.FillSlot(weapon);
    }

    public void RemoveSecondaryWeapon()
    {
        secondaryWeaponSlot.ClearSlot();
    }

    public void SwitchWeapons()
    {
        // TODO
    }
    
    // TODO item display
    
}

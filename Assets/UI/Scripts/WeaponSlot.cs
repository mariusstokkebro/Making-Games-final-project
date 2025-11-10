using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    private Sprite WeaponIcon;
    private int maxUses;
    private int usesLeft;
    
    void FillSlot(Weapon weapon)
    {
        WeaponIcon = weapon.sprite;
        maxUses = weapon.usesUntilBreak;
        usesLeft = maxUses;
    }

    void ClearSlot()
    {
        
    }

    void ReduceUses()
    {
        --usesLeft;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

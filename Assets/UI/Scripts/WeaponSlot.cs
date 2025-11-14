using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private float barRotation = 1f;
    [SerializeField] private Image IconImage;
    private Sprite IconSprite;
    private int maxUses;
    private int usesLeft;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Image>().material.SetFloat("_Rotation", barRotation);
    }
    
    public void FillSlot(Weapon weapon)
    {
        IconImage.enabled = true;
        IconImage.sprite = weapon.sprite;
        maxUses = weapon.usesUntilBreak;
        usesLeft = maxUses;
        
        GetComponent<Image>().material.SetFloat("_MaxUses", maxUses);
        GetComponent<Image>().material.SetFloat("_UsesLeft", usesLeft);
        
    }

    public void ClearSlot()
    {
        GetComponent<Image>().material.SetFloat("_UsesLeft", 0);
        IconImage.enabled = false;
    }

    public void ReduceUses()
    {
        Debug.Log("Weapon used");
        --usesLeft;
        GetComponent<Image>().material.SetFloat("_UsesLeft", usesLeft);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

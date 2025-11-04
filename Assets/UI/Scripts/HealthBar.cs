using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Material healthBarMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBarMaterial = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(float maxHP, float HPperIcon)
    {
        GetComponent<Image>().material.SetFloat("_MaxHealth", maxHP);
        GetComponent<Image>().material.SetFloat("_HealthPerIcon", HPperIcon);
        UpdateHealthBar(maxHP);
        
        Debug.Log("maxHP "+ maxHP + ", "+HPperIcon+" per icon");
    }

    public void UpdateHealthBar(float newHP)
    {
        GetComponent<Image>().material.SetFloat("_CurrentHealth", newHP);
        Debug.Log("New HP value: "+ newHP);
    }
}

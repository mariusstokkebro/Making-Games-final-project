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
        healthBarMaterial.SetFloat("_MaxHealth", maxHP);
        healthBarMaterial.SetFloat("_HealthPerIcon", HPperIcon);
        UpdateHealthBar(maxHP);
    }

    public void UpdateHealthBar(float newHP)
    {
        healthBarMaterial.SetFloat("_CurrentHealth", newHP);
    }
}

using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthBarIcon healthBarIcon;

    private HealthBarIcon[] icons;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int maxHealth)
    {
        icons = new HealthBarIcon[maxHealth];
        float iconWidth = healthBarIcon.GetComponent<RectTransform>().sizeDelta.x * healthBarIcon.transform.localScale.x;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i] = Instantiate(healthBarIcon, transform);
            icons[i].transform.SetLocalPositionAndRotation(icons[i].transform.localPosition + Vector3.right * i * iconWidth, Quaternion.identity);
            icons[i].transform.SetPositionAndRotation(icons[i].transform.position, Quaternion.identity);
        }
    }

    public void UpdateHealthBar(int newHP)
    {
        
    }
}

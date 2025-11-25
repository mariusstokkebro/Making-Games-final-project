using UnityEngine;
using TMPro;
public class StatSlot : MonoBehaviour
{
    public EntityStats statType;


    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private TextMeshProUGUI valueText;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void SetValue(float value)
    {
        valueText.text = value.ToString("0");
        //AnimateIncrease();
        Debug.Log($"Stat {statType} updated to {value}");
    }


}

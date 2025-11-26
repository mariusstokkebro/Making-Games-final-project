using UnityEngine;
using TMPro;
public class StatSlot : MonoBehaviour
{
    public EntityStats statType;


    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private TextMeshProUGUI valueText;

    [SerializeField] private Color originalColor;
    private Color flashColor;
    [SerializeField] private float flashDuration = 1.0f;
    float value;
    private float flashTimer = 0f;
    private bool isFlashing = false;

    private void Awake()
    {
        if (valueText != null)
            originalColor = valueText.color;
    }

    private void Update()
    {
        //return to original color after flash
        if (!isFlashing) return;

        flashTimer += Time.deltaTime;
        float t = flashTimer / flashDuration;

        valueText.color = Color.Lerp(flashColor, originalColor, t);

        if (t >= 1f)
            isFlashing = false;
    }

    public void SetValue(float newValue, float oldValue)
    {
        value = newValue;
        valueText.text = value.ToString("0");

        if (newValue > oldValue)
        {
            StartFlash(Color.green);
        }
        else if (newValue < oldValue)
        {
            StartFlash(Color.red);
        }

    }
    public float GetValue()
    {
        return value;
    }
    private void StartFlash(Color color)
    {
        flashColor = color;
        valueText.color = flashColor;

        flashTimer = 0f;
        isFlashing = true;
    }


}

using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text descriptionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        tooltipPanel.SetActive(false);
    }

    private void Update()
    {
    }

    public void ShowTooltip(string description)
    {
        descriptionText.text = description;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}

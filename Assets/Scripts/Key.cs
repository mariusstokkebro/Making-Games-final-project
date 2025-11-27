using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public void Interact(PlayerScript player)
    {
        Debug.Log("Key picked up");
        player.HasKey = true;
        HUD.Instance.ShowKeyDisplay();
        TooltipManager.Instance.HideTooltip();
        Destroy(gameObject);

    }
}

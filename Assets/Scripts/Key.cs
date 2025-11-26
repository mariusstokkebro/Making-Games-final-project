using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public void Interact(PlayerScript player)
    {
        Debug.Log("Key picked up");
        player.HasKey = true;
        TooltipManager.Instance.HideTooltip();
        Destroy(gameObject);

    }
}

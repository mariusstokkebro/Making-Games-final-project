using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public AudioClip keySound;
    public void Interact(PlayerScript player)
    {
        AudioManager.Instance.PlaySFX(keySound);
        Debug.Log("Key picked up");
        player.HasKey = true;
        HUD.Instance.ShowKeyDisplay();
        TooltipManager.Instance.HideTooltip();
        Destroy(gameObject);

    }
}

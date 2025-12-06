using UnityEngine;

public class StaircaseScript : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer lockSprite;
    public AudioClip unlockDoor;

    public void Interact(PlayerScript player)
    {
        if (player.HasKey)
        {
            AudioManager.Instance.PlaySFX(unlockDoor, volumeScale: 2f);
            lockSprite.enabled = false;
            LevelGeneration.Instance.generateNextLevel();
            LevelManager.Instance.ResetLevelProgress();
            player.HasKey = false;
            HUD.Instance.HideKeyDisplay();
        }
        else
        {
            Debug.Log("You need a key to use the staircase.");
        }

    }
    public string GetDescription() => "Interact with me once you have the key";
}
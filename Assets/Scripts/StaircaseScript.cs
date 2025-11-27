using UnityEngine;

public class StaircaseScript : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer lockSprite;

    public void Interact(PlayerScript player)
    {
        if (player.HasKey)
        {
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
}
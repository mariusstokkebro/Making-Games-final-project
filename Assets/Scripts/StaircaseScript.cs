using UnityEngine;

public class StaircaseScript : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer lockSprite;
    [SerializeField] private float displayDuration = 1f;

    private float timer = 0f;
    private bool showing = false;

    private void Update()
    {
        if (!showing) return;

        timer += Time.deltaTime;
        if (timer >= displayDuration)
        {
            lockSprite.enabled = false;
            showing = false;
        }
    }

    public void Interact(PlayerScript player)
    {
        if (player.HasKey)
        {
            LevelGeneration.Instance.generateNextLevel();
            LevelManager.Instance.ResetLevelProgress();
            player.HasKey = false;
        }
        else
        {
            Debug.Log("You need a key to use the staircase.");
            LockPopUp();
        }

    }

    private void LockPopUp()
    {
        lockSprite.enabled = true;
        timer = 0f;
        showing = true;
    }
}
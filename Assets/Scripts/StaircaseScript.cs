using UnityEngine;

public class StaircaseScript : MonoBehaviour, IInteractable
{
    public void Interact(PlayerScript player)
    {
        if (player.HasKey)
        {
            LevelGeneration.Instance.generateNextLevel();
            LevelManager.Instance.ResetLevelProgress();
        }
        else
        {
            Debug.Log("You need a key to use the staircase.");
        }

    }
}
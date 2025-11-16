using UnityEngine;

public class StaircaseScript : MonoBehaviour, IInteractable
{
    public void Interact(PlayerScript player)
    {
        LevelGeneration.Instance.generateNextLevel();

    }
}

using System;
using UnityEngine;

public class ActiveItemScript : MonoBehaviour
{
    [SerializeField] private ActiveItem activeItem;

    public ActiveItemScript(ActiveItem item)
    {
        activeItem = item;
    }

    private void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = activeItem.mesh;
    }

    /// <summary>
    /// A one-time effect when picking up item, e.g. unlocking the dash
    /// </summary>
    public void OnPickup(PlayerScript p)
    {
        p.SetSecondaryActiveItem(activeItem);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            OnPickup(player);
        }
    }
}

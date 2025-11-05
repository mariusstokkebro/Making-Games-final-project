using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponScript : MonoBehaviour
{
    [FormerlySerializedAs("activeItem")] [SerializeField] private Weapon weapon;

    public WeaponScript(Weapon item)
    {
        weapon = item;
    }

    private void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = weapon.mesh;
    }

    /// <summary>
    /// A one-time effect when picking up item, e.g. unlocking the dash
    /// </summary>
    public void OnPickup(PlayerScript p)
    {
        p.SetSecondaryWeapon(weapon);
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

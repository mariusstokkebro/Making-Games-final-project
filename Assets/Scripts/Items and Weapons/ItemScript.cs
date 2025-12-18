using System;
using Items_and_Weapons.Effects;
using UnityEngine;

namespace Items_and_Weapons
{
    public class ItemScript : MonoBehaviour, IInteractable
    {
#nullable enable
        [SerializeField] internal BaseItem? item;

        public void SetItem(BaseItem item)
        {
            this.item = item;
        }

        private void Start()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = item?.sprite;
            transform.rotation = Quaternion.Euler(0, -45, 0);
            transform.localScale = new Vector3(10, 10, 10);
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            GameObject currentRoom = LevelManager.Instance.GetCurrentRoom();
            transform.parent = currentRoom.transform.Find("roomLayout");

        }
        /// <summary>
        /// A one-time effect when picking up item, e.g. unlocking the dash
        /// </summary>
        public void Interact(PlayerScript p)
        {
            if (item == null)
            {
                Destroy(gameObject);
                return;
            }

            if (item is PassiveItemData passive)
            {
                foreach (PassiveEffect effect in passive.effects)
                {
                    effect.Apply(p);
                }

                p.AddPassiveItem(passive);
            }
            else if (item is Weapon weapon)
            {
                p.SetSecondaryWeapon(weapon);
            }

            Destroy(gameObject);
        }

        public string GetDescription() => item?.GetDescription() ?? "";
    }
}

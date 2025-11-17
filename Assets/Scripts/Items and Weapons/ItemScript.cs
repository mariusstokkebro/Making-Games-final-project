using Items_and_Weapons.Effects;
using UnityEngine;

namespace Items_and_Weapons
{
    public class ItemScript : MonoBehaviour
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
            transform.localScale += (new Vector3(3, 3, 3));
        }

        /// <summary>
        /// A one-time effect when picking up item, e.g. unlocking the dash
        /// </summary>
        public void OnPickup(PlayerScript p)
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

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
                OnPickup(player);
            }
        }

        // Think this is best done by having some unlockables in the player? effect.Apply(p) => p.unlockDash();
        // /// <summary>
        // /// Activate the item's effect, e.g. Dashing
        // /// </summary>
        // /// <typeparam name="T"></typeparam>
        // public T? Activate<T>()
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}

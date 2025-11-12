using Items_and_Weapons.Effects;
using UnityEngine;

namespace Items_and_Weapons
{
    public class PassiveItemScript : MonoBehaviour
    {
        #nullable enable
        [SerializeField] internal PassiveItemData? itemData;

        public void SetItem(PassiveItemData item)
        {
            itemData = item;
        }

        private void Start()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = itemData?.sprite;
            transform.rotation = Quaternion.Euler(0, -45, 0);
            transform.localScale += (new Vector3(3, 3, 3));
        }

        /// <summary>
        /// A one-time effect when picking up item, e.g. unlocking the dash
        /// </summary>
        public void OnPickup(PlayerScript p)
        {
            // _itemData will never be null because an enemy with no drop doesn't spawn a lootPrefab
            if (itemData == null) {Destroy(gameObject); return;}

            foreach (PassiveEffect effect in itemData!.effects)
            {
                effect.Apply(p);
            }

            p.AddPassiveItem(itemData);
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

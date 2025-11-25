using UnityEngine;

public class StatHUD : MonoBehaviour
{
    [SerializeField] private StatSlot[] statSlotElements;

    private BaseEntity playerEntity;

    private void Start()
    {
        playerEntity = GameObject.FindWithTag("Player").GetComponent<BaseEntity>();
        playerEntity.OnStatChanged += UpdateStatDisplay;

        // Initialize all values once at start
        foreach (StatSlot statSlot in statSlotElements)
        {
            switch (statSlot.statType)
            {
                case EntityStats.Damage:
                    statSlot.SetValue(playerEntity.GetDamage());
                    break;
                case EntityStats.Speed:
                    statSlot.SetValue(playerEntity.GetMovementSpeed());
                    break;
            }
        }
    }

    private void UpdateStatDisplay(EntityStats type, float newValue)
    {
        foreach (StatSlot statSlot in statSlotElements)
        {
            if (statSlot.statType == type)
            {
                statSlot.SetValue(newValue);
                return;
            }
        }
    }
}


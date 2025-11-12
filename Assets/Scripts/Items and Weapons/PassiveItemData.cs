using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "Scriptable Objects/Passive Item")]
public class PassiveItemData : BaseItem
{
    public Sprite sprite;
    public PassiveEffect[] effects;

    public override string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(itemName);

        if (effects != null && effects.Length > 0)
        {
            sb.AppendLine("Effects:");
            foreach (var effect in effects)
            {
                if (effect != null)
                    sb.AppendLine($"- {effect.GetDescription()}");
            }
        }
        else
        {
            sb.AppendLine("No effects");
        }

        return sb.ToString();
    }
}

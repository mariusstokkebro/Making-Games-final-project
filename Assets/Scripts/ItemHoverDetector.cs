using Items_and_Weapons;
using UnityEngine;

public class ItemHoverDetector : MonoBehaviour
{
    public LayerMask layerMask = ~0;
    public float maxDistance = 200f;

    void Update()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, layerMask, QueryTriggerInteraction.Collide);
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (var h in hits)
        {
            if (h.collider.TryGetComponent(out ItemScript passiveItemScript))
            {
                TooltipManager.Instance.ShowTooltip(passiveItemScript.item?.GetDescription());
                return;
            }
        }

        TooltipManager.Instance.HideTooltip();
    }
}

using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private InventorySO activeSkillInventory;
    [SerializeField] private InventorySO passiveSkillInventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<LootItem>(out LootItem item))
            PickItemWithMagnetEffect(item);

        if (collision.gameObject.TryGetComponent<Exp>(out Exp exp))
        {
            exp.SetTarget(transform.parent.position);
            Debug.Log("Collect Exp");
        }
    }

    private void PickItemWithMagnetEffect(LootItem item)
    {
        if (item.inventoryItem.ItemType == ItemType.ActiveSkill)
        {
            activeSkillInventory.AddItem(item.inventoryItem, item.quantity);
            item.SetTarget(transform.parent.position);
            return;
        }
        else if (item.inventoryItem.ItemType == ItemType.PassiveSkill)
        {
            passiveSkillInventory.AddItem(item.inventoryItem, item.quantity);
            item.SetTarget(transform.parent.position);
            return;
        }
    }
}

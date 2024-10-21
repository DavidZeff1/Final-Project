using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item;
    public int quantity = 1;
    private Animator animator;
    private bool isCollected = false;

    void Start()
    {
        if (item.itemType != ItemType.WEAPON)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (item.itemType != ItemType.WEAPON)
            {
                OnCollect();
            }

            InventoryManager.Instance.AddItem(item, quantity);
            Destroy(gameObject);
        }
    }

    public void OnCollect()
    {
        if (isCollected)
        {
            return; 
        }

        isCollected = true;
        if (animator != null && item.itemType != ItemType.WEAPON)
        {
            animator.SetBool("IsCollected", true);
        }
        
    }
    
}
